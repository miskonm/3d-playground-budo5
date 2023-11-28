using Zenject;

namespace Playground.Services.AppState
{
    public class AppStateServiceInstaller : Installer<AppStateServiceInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
            Container
                .Bind<AppStateService>()
                .FromSubContainerResolve()
                .ByMethod(InstallService)
                .AsSingle();
        }

        #endregion

        #region Private methods

        private void InstallService(DiContainer subContainer)
        {
            subContainer.Bind<AppStateService>().AsSingle();

#if UNITY_EDITOR
            subContainer.Bind<AppStateProvider>().To<EditorAppStateProvider>().AsSingle();
#else
            subContainer.Bind<AppStateProvider>().To<MobileAppStateProvider>().AsSingle();
#endif
        }

        #endregion
    }
}