using Playground.DI;

namespace Playground.Services.Command
{
    public class CommandServiceInstaller : Installer<CommandServiceInstaller>
    {
        protected override void InstallBindings()
        {
            Container.Bind<CommandService>().FromNewGameObject().NonLazy();
        }
    }
}