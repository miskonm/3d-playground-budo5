using Playground.DI;

namespace Playground.Services.AppState
{
    public class AppStateServiceInstaller : Installer<AppStateServiceInstaller>
    {
        #region Protected methods

        protected override void InstallBindings()
        {
            Container.Bind<AppStateService>();

#if UNITY_EDITOR
            Container.Bind<AppStateProvider>().To<EditorAppStateProvider>();
#else
            Container.Bind<AppStateProvider>().To<MobileAppStateProvider>();
#endif
        }

        #endregion
    }
}