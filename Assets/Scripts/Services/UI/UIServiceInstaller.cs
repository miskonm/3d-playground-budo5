using Zenject;

namespace Playground.Services.UI
{
    public class UIServiceInstaller : Installer<UIServiceInstaller>
    {
        #region Variables

        private const string UILayerProviderPath = "UI/UIService";

        #endregion

        #region Public methods

        public override void InstallBindings()
        {
            Container
                .Bind<UIService>()
                .FromSubContainerResolve()
                .ByMethod(InstallService)
                .AsSingle();
        }

        #endregion

        #region Private methods

        private void InstallService(DiContainer subContainer)
        {
            subContainer.Bind<UIService>().AsSingle();
            subContainer.Bind<UIScreenProvider>().AsSingle();
            subContainer.Bind<UILayerProvider>().FromComponentInNewPrefabResource(UILayerProviderPath).AsSingle();
        }

        #endregion
    }
}