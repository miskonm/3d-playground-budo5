using Zenject;

namespace Playground.Services.Game
{
    public class GameServiceInstaller : Installer<GameServiceInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<GameService>().AsSingle();
        }

        #endregion
    }
}