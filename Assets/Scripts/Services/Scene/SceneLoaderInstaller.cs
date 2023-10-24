using Zenject;

namespace Playground.Services.Scene
{
    public class SceneLoaderInstaller : Installer<SceneLoaderInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
        }

        #endregion
    }
}