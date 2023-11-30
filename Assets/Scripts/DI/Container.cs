using System;
using System.Collections.Generic;

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
                if (info.IsNonLazy)
                {
                    Instantiate(info);
                }
            }
        }

        #endregion

        #region Private methods

        private void Instantiate(BindInfo bindInfo)
        {
            // TODO: Right realization for ctor arguments with check circular dependency
            Type instanceType = bindInfo.RealisationType ?? bindInfo.BindType;
            object instance = Activator.CreateInstance(instanceType);
            _instancesByTypes.Add(bindInfo.BindType, instance);
        }

        #endregion
    }
}