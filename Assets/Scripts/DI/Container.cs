using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Playground.DI
{
    public class Container : IInstantiator
    {
        #region Variables

        private readonly Dictionary<Type, BindInfo> _bindInfoByTypes = new();
        private readonly Transform _contextTransform;
        private readonly Dictionary<Type, object> _instancesByTypes = new();

        #endregion

        #region Setup/Teardown

        public Container(Transform contextTransform)
        {
            _contextTransform = contextTransform;
            _instancesByTypes.Add(typeof(IInstantiator), this);
        }

        #endregion

        #region IInstantiator

        public T Instantiate<T>()
        {
            // TODO:
            return default;
        }

        public object Instantiate(Type type)
        {
            return null;
        }

        public TComponent InstantiatePrefab<TComponent>(GameObject prefab, Transform transform)
            where TComponent : MonoBehaviour
        {
            return InstantiatePrefab(typeof(TComponent), prefab, transform) as TComponent;
        }

        public TComponent InstantiatePrefab<TComponent>(Component prefab, Transform transform)
            where TComponent : MonoBehaviour
        {
            Component component = Object.Instantiate(prefab, transform);
            TryInjectObject(component);
            return component as TComponent;
        }

        #endregion

        #region Public methods

        public BindInfo Bind<T>()
        {
            Type bindType = typeof(T);

            if (_bindInfoByTypes.ContainsKey(bindType))
            {
                throw new Exception($"Try to bind several instances of type '{bindType}'");
            }

            BindInfo bindInfo = new()
            {
                BindType = bindType,
            };

            _bindInfoByTypes.Add(bindType, bindInfo);

            return bindInfo;
        }

        public void InjectAllSceneObjects()
        {
            MonoBehaviour[] objects =
                Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (MonoBehaviour obj in objects)
            {
                TryInjectObject(obj);
            }
        }

        public void InstantiateNonLazy()
        {
            foreach (BindInfo info in _bindInfoByTypes.Values)
            {
                if (info.IsNonLazy && !_instancesByTypes.ContainsKey(info.BindType))
                {
                    Instantiate(info);
                }
            }
        }

        #endregion

        #region Private methods

        private object CreatePureInstance(BindInfo info, object[] arguments)
        {
            return arguments == null
                ? Activator.CreateInstance(info.GetInstanceType())
                : Activator.CreateInstance(info.GetInstanceType(), arguments);
        }

        private object[] GetArgumentsInstances(List<Type> dependencyStack, ParameterInfo[] ctorParametersInfo)
        {
            object[] arguments = new object[ctorParametersInfo.Length];
            for (int i = 0; i < ctorParametersInfo.Length; i++)
            {
                ParameterInfo parameterInfo = ctorParametersInfo[i];
                Type type = parameterInfo.ParameterType;

                if (!_instancesByTypes.TryGetValue(type, out object value))
                {
                    if (!_bindInfoByTypes.ContainsKey(type))
                    {
                        throw new Exception($"Can't instantiate type '{type}' because it's not bind!");
                    }

                    if (dependencyStack.Contains(type))
                    {
                        ThrowCircularDependencyException(dependencyStack, type);
                    }

                    value = Instantiate(_bindInfoByTypes[type], dependencyStack);
                }

                arguments[i] = value;
            }

            return arguments;
        }

        private ParameterInfo[] GetConstructorParameterInfos(Type type)
        {
            MemberInfo[] member = type.GetMember(".ctor");
            if (member.Length > 0)
            {
                MethodBase ctorMethod = member[0] as MethodBase;
                if (ctorMethod != null)
                {
                    return ctorMethod.GetParameters();
                }
            }

            return null;
        }

        private ParameterInfo[] GetCtorParametersInfo(Type instanceType)
        {
            return !IsMonoBehaviour(instanceType)
                ? GetConstructorParameterInfos(instanceType)
                : GetMethodParameterInfos(instanceType, out _);
        }

        private object GetInstance(Type type)
        {
            if (!_instancesByTypes.TryGetValue(type, out object obj))
            {
                obj = InstantiateInternal(type);
            }

            return obj;
        }

        private ParameterInfo[] GetMethodParameterInfos(Type type, out MethodInfo methodInfo)
        {
            methodInfo = null;

            MethodInfo[] methodInfos = type.GetMethods();
            foreach (MethodInfo method in methodInfos)
            {
                InjectAttribute customAttribute = method.GetCustomAttribute<InjectAttribute>();
                if (customAttribute != null)
                {
                    methodInfo = method;
                    break;
                }
            }

            return methodInfo == null ? null : methodInfo.GetParameters();
        }

        private object InstantiateInternal(Type type)
        {
            if (!_bindInfoByTypes.ContainsKey(type))
            {
                throw new Exception($"Can't instantiate type '{type}' because it's not bind!");
            }

            return Instantiate(_bindInfoByTypes[type]);
        }

        private object Instantiate(BindInfo bindInfo, List<Type> dependencyStack = null)
        {
            Type instanceType = bindInfo.GetInstanceType();

            ParameterInfo[] ctorParametersInfo = GetCtorParametersInfo(instanceType);

            object instance;
            if (ctorParametersInfo == null || ctorParametersInfo.Length == 0)
            {
                instance = Instantiate(bindInfo, arguments: null);
            }
            else
            {
                dependencyStack ??= new List<Type>();
                dependencyStack.Add(instanceType);

                object[] arguments = GetArgumentsInstances(dependencyStack, ctorParametersInfo);
                instance = Instantiate(bindInfo, arguments);
            }

            _instancesByTypes.Add(bindInfo.BindType, instance);

            return instance;
        }

        private object Instantiate(BindInfo info, object[] arguments)
        {
            return info.BindMethod == BindMethod.Instance
                ? CreatePureInstance(info, arguments)
                : InstantiateGameObject(info);
        }

        private object InstantiateGameObject(BindInfo info)
        {
            Type instanceType = info.GetInstanceType();
            if (info.BindMethod == BindMethod.FromPrefab)
            {
                return InstantiatePrefab(instanceType, info.Prefab, _contextTransform);
            }

            GameObject go = new(instanceType.Name);
            go.transform.SetParent(_contextTransform);
            Component addComponent = go.AddComponent(instanceType);
            TryInjectObject(addComponent);
            return addComponent;
        }

        private Object InstantiatePrefab(Type type, GameObject prefab, Transform transform)
        {
            GameObject go = Object.Instantiate(prefab, transform);
            Component component = go.GetComponent(type);
            TryInjectObject(component);

            return component;
        }

        private bool IsMonoBehaviour(Type instanceType)
        {
            if (instanceType.BaseType == null)
            {
                return false;
            }

            if (instanceType.BaseType == typeof(MonoBehaviour))
            {
                return true;
            }

            return IsMonoBehaviour(instanceType.BaseType);
        }

        private static void ThrowCircularDependencyException(List<Type> dependencyStack, Type type)
        {
            string stackString = string.Empty;
            for (int j = dependencyStack.Count - 1; j >= 0; j--)
            {
                stackString += $"{dependencyStack[j].Name}";

                if (j != 0)
                {
                    stackString += "\n";
                }
            }

            throw new Exception($"Circular dependency for type '{type.Name}' with stack:\n{stackString}");
        }

        private void TryInjectObject(Component obj)
        {
            Type type = obj.GetType();
            ParameterInfo[] parameterInfos = GetMethodParameterInfos(type, out MethodInfo methodInfo);

            if (parameterInfos == null || parameterInfos.Length == 0)
            {
                return;
            }

            object[] arguments = new object[parameterInfos.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = GetInstance(parameterInfos[i].ParameterType);
            }

            methodInfo.Invoke(obj, arguments);
        }

        #endregion
    }
}