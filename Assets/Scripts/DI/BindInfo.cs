using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Playground.DI
{
    public class BindInfo
    {
        #region Variables

        public Type BindType;
        public BindMethod BindMethod;
        public bool IsNonLazy;
        public Type ToType;
        public GameObject Prefab;

        #endregion

        #region Public methods

        public Type GetInstanceType()
        {
            return ToType ?? BindType;
        }

        public BindInfo FromNewGameObject()
        {
            BindMethod = BindMethod.FromNewGameObject;
            return this;
        }

        public BindInfo FromPrefab(GameObject prefab)
        {
            Prefab = prefab;
            BindMethod = BindMethod.FromPrefab;
            return this;
        }

        public BindInfo NonLazy()
        {
            IsNonLazy = true;
            return this;
        }

        public BindInfo To<T>()
        {
            ToType = typeof(T);
            return this;
        }

        #endregion
    }
}