using Playground.DI;

namespace Playground.Services.Save
{
    public class GameDataServiceInstaller : Installer<GameDataServiceInstaller>
    {
        protected override void InstallBindings()
        {
            Container.Bind<GameDataService>().FromNewGameObject();
        }
    }
}