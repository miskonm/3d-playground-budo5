

using Playground.DI;

namespace Playground.Services.GameManagement
{
    public class GameManagementServiceInstaller : Installer<GameManagementServiceInstaller>
    {
        #region Public methods

        protected override void InstallBindings()
        {
            // TODO: Nikita remove non lazy
            Container.Bind<GameManagementService>().NonLazy();
        }

        #endregion
    }
}