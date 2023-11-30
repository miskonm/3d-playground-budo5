using Playground.Services.Log;
using Playground.Services.Scene;

namespace Playground.Infrastructure.Installers
{
    public class ProjectInstaller2 : Playground.DI.Installer
    {
        #region Public methods

        protected override void InstallBindings()
        {
            this.LogError($"InstallBindings");
            SceneLoaderInstaller.Install(Container);
            // GameManagementServiceInstaller.Install(Container);
            // EventBusInstaller.Install(Container);
            // AudioServiceInstaller.Install(Container);
            // UIServiceInstaller.Install(Container);
            // GameDataServiceInstaller.Install(Container);
            // AppStateServiceInstaller.Install(Container);
        }

        #endregion
    }
}