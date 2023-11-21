using Playground.Game;
using Playground.Services.Input;
using Zenject;

namespace Playground.Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            InputServiceInstaller.Install(Container);

            Container.Bind<GameScreenService>().AsSingle();
            Container.Bind<SettingsScreenController>().AsSingle();
        }

        #endregion
    }
}