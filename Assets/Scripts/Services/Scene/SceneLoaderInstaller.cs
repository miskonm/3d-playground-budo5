

using Playground.DI;
using Playground.Services.Log;

namespace Playground.Services.Scene
{
    public class SceneLoaderInstaller : Installer<SceneLoaderInstaller>
    {
        #region Public methods

        protected override void InstallBindings()
        {
            this.LogError($"InstallBindings");
            Container.Bind<SceneLoader>().NonLazy();
        }

        #endregion
    }
}