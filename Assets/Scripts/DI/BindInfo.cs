using System;

namespace Playground.DI
{
    public class BindInfo
    {
        #region Variables

        public Type BindType;
        public Type ToType;
        public bool IsNonLazy;

        #endregion

        #region Public methods

        public BindInfo To<T>()
        {
            ToType = typeof(T);
            return this;
        }
        
        public BindInfo NonLazy()
        {
            IsNonLazy = true;
            return this;
        }

        #endregion
    }
}