using Zenject;

namespace Playground.Services.Save
{
    public class GameDataServiceInstaller : Installer<GameDataServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameDataService>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}