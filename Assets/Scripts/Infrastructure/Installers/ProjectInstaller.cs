using Playground.Audio;
using Playground.Services.Event;
using Playground.Services.GameManagement;
using Playground.Services.Scene;
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
        }

        #endregion
    }
}