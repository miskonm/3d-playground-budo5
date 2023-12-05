using Playground.Game;
using Playground.Services.AppState;
using Playground.Services.Audio;
using Playground.Services.Bootstrap;
using Playground.Services.Event;
using Playground.Services.GameManagement;
using Playground.Services.Input;
using Playground.Services.Log;
using Playground.Services.Save;
using Playground.Services.Scene;
using Playground.Services.UI;

namespace Playground.Infrastructure.Installers
{
    public class ProjectInstaller : DI.Installer
    {
        #region Public methods

        protected override void InstallBindings()
        {
            this.LogError($"InstallBindings");
            GameManagementServiceInstaller.Install(Container);
            SceneLoaderInstaller.Install(Container);
            EventBusInstaller.Install(Container);
            InputServiceInstaller.Install(Container);
            AudioServiceInstaller.Install(Container);
            UIServiceInstaller.Install(Container);
            GameDataServiceInstaller.Install(Container);
            AppStateServiceInstaller.Install(Container);
            BootstrapServiceInstaller.Install(Container);

            Container.Bind<GameScreenService>();
            Container.Bind<SettingsScreenController>();
        }

        #endregion
    }
}