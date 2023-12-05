using System;
using System.Collections.Generic;
using System.Reflection;

namespace Playground.DI
{
    public class Container
    {
        #region Variables

        private readonly Dictionary<Type, BindInfo> _bindInfoByTypes = new();
        private readonly Dictionary<Type, object> _instancesByTypes = new();

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

        private ParameterInfo[] GetCtorParametersInfo(Type instanceType)
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

            return null;
        }

        private object Instantiate(BindInfo bindInfo)
        {
            Type instanceType = bindInfo.ToType ?? bindInfo.BindType;
            
            ParameterInfo[] ctorParametersInfo = GetCtorParametersInfo(instanceType);
            
            object instance = null;
            if (ctorParametersInfo == null || ctorParametersInfo.Length == 0)
            {
                instance = Activator.CreateInstance(instanceType); 
            }
            else
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

                        value = Instantiate(_bindInfoByTypes[type]);
                    }
                    
                    arguments[i] = value;
                }

                instance = Activator.CreateInstance(instanceType, arguments); 
            }
            
            _instancesByTypes.Add(bindInfo.BindType, instance);

            return instance;
        }

        #endregion
    }
}