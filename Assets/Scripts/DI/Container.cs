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

        public Object InstantiatePrefab(Type type, GameObject prefab, Transform transform)
        {
            GameObject go = Object.Instantiate(prefab, transform);
            Component component = go.GetComponent(type);
            TryInjectObject(component);

            return component;
        }

        #endregion

        #region Private methods

        private ParameterInfo[] GetCtorParametersInfo(Type instanceType)
        {
            bool isMono = IsMonobeh(instanceType);

            if (!isMono)
            {
                MemberInfo[] member = instanceType.GetMember(".ctor");
                if (member.Length > 0)
                {
                    MethodBase ctorMethod = member[0] as MethodBase;
                    if (ctorMethod != null)
                    {
                        return ctorMethod.GetParameters();
                    }
                }
            }
            else
            {
                MethodInfo[] methodInfos = instanceType.GetMethods();
                MethodInfo injectMethod = null;
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    InjectAttribute customAttribute = methodInfo.GetCustomAttribute<InjectAttribute>();
                    if (customAttribute != null)
                    {
                        injectMethod = methodInfo;
                        break;
                    }
                }

                if (injectMethod == null)
                {
                    return null;
                }

                return injectMethod.GetParameters();
            }

            return null;
        }

        private object Instantiate(Type type)
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

            object instance = null;
            if (ctorParametersInfo == null || ctorParametersInfo.Length == 0)
            {
                instance = InstantiateWithoutParameters(bindInfo);
            }
            else
            {
                object[] arguments = new object[ctorParametersInfo.Length];

                dependencyStack ??= new List<Type>();
                dependencyStack.Add(instanceType);

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
                            string stackString = string.Empty;
                            for (int j = dependencyStack.Count - 1; j >= 0; j--)
                            {
                                stackString += $"{dependencyStack[j].Name}";

                                if (j != 0)
                                {
                                    stackString += "\n";
                                }
                            }

                            throw new Exception(
                                $"Circular dependency for type '{type.Name}' with stack:\n{stackString}");
                        }

                        value = Instantiate(_bindInfoByTypes[type], dependencyStack);
                    }

                    arguments[i] = value;
                }

                instance = InstantiateWithParameters(bindInfo, arguments);
            }

            _instancesByTypes.Add(bindInfo.BindType, instance);

            return instance;
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

        private object InstantiateWithoutParameters(BindInfo info)
        {
            object instance;
            if (info.BindMethod == BindMethod.Instance)
            {
                instance = Activator.CreateInstance(info.GetInstanceType());
            }
            else
            {
                instance = InstantiateGameObject(info);
            }

            return instance;
        }

        private object InstantiateWithParameters(BindInfo info, object[] arguments)
        {
            object instance;

            if (info.BindMethod == BindMethod.Instance)
            {
                instance = Activator.CreateInstance(info.GetInstanceType(), arguments);
            }
            else
            {
                instance = InstantiateGameObject(info);
            }

            return instance;
        }

        private bool IsMonobeh(Type instanceType)
        {
            if (instanceType.BaseType == null)
            {
                return false;
            }

            if (instanceType.BaseType == typeof(MonoBehaviour))
            {
                return true;
            }

            return IsMonobeh(instanceType.BaseType);
        }

        private void TryInjectObject(Component obj)
        {
            Type type = obj.GetType();
            MethodInfo[] methodInfos = type.GetMethods();
            MethodInfo injectMethod = null;
            foreach (MethodInfo methodInfo in methodInfos)
            {
                InjectAttribute customAttribute = methodInfo.GetCustomAttribute<InjectAttribute>();
                if (customAttribute != null)
                {
                    injectMethod = methodInfo;
                    break;
                }
            }

            if (injectMethod == null)
            {
                return;
            }

            ParameterInfo[] parameterInfos = injectMethod.GetParameters();

            if (parameterInfos.Length == 0)
            {
                return;
            }

            object[] arguments = new object[parameterInfos.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = GetInstance(parameterInfos[i].ParameterType);
            }

            injectMethod.Invoke(obj, arguments);
        }

        private object GetInstance(Type type)
        {
            if (!_instancesByTypes.TryGetValue(type, out object obj))
            {
                obj = Instantiate(type);
            }

            return obj;
        }

        #endregion
    }
}