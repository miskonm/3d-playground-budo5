using Playground.DI;

namespace Playground.Services.Gameplay
{
    public class GameplayServiceInstaller : Installer<GameplayServiceInstaller>
    {
        #region Protected methods

        protected override void InstallBindings()
        {
            Container.Bind<GameplayService>();

            BindSubServices();
        }

        #endregion

        #region Private methods

        private void BindSubServices() { }

        #endregion
    }
}