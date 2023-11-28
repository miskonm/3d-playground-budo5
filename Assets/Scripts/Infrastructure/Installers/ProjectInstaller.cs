using Playground.Services.AppState;
using Playground.Services.Audio;
using Playground.Services.Event;
using Playground.Services.GameManagement;
using Playground.Services.Save;
using Playground.Services.Scene;
using Playground.Services.UI;
using Zenject;

namespace Playground.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            SceneLoaderInstaller.Install(Container);
            GameManagementServiceInstaller.Install(Container);
            EventBusInstaller.Install(Container);
            AudioServiceInstaller.Install(Container);
            UIServiceInstaller.Install(Container);
            GameDataServiceInstaller.Install(Container);
            AppStateServiceInstaller.Install(Container);
        }

        #endregion
    }
}