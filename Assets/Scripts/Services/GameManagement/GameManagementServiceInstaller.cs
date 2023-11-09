using Zenject;

namespace Playground.Services.GameManagement
{
    public class GameManagementServiceInstaller : Installer<GameManagementServiceInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<GameManagementService>().AsSingle();
        }

        #endregion
    }
}