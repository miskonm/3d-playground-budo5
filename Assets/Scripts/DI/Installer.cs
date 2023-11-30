using System;
using UnityEngine;

namespace Playground.DI
{
    public abstract class Installer : MonoBehaviour
    {
        #region Properties

        protected Container Container { get; private set; }

        #endregion

        #region Public methods

        public void Install(Container container)
        {
            Container = container;
            InstallBindings();
        }

        #endregion

        #region Protected methods

        protected abstract void InstallBindings();

        #endregion
    }

    public abstract class Installer<T> where T : Installer<T>
    {
        #region Properties

        protected Container Container { get; private set; }

        #endregion

        #region Public methods

        public static void Install(Container container)
        {
            T installer = Activator.CreateInstance<T>();
            installer.Initialize(container);
        }

        #endregion

        #region Protected methods

        protected abstract void InstallBindings();

        #endregion

        #region Private methods

        private void Initialize(Container container)
        {
            Container = container;
            InstallBindings();
        }

        #endregion
    }
}