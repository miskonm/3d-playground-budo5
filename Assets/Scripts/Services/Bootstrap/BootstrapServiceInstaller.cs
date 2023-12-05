

using Playground.DI;

namespace Playground.Services.Bootstrap
{
    public class BootstrapServiceInstaller : Installer<BootstrapServiceInstaller>
    {
        protected override void InstallBindings()
        {
            Container.Bind<BootstrapService>();
        }
    }
}