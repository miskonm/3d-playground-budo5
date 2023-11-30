using System;

namespace Playground.DI
{
    public class BindInfo
    {
        #region Variables

        public Type BindType;
        public Type RealisationType;
        public bool IsNonLazy;

        #endregion

        #region Public methods

        public BindInfo To<T>()
        {
            RealisationType = typeof(T);
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