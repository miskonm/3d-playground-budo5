using Zenject;

namespace Playground.Services.Bootstrap
{
    public class BootstrapServiceInstaller : Installer<BootstrapServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapService>().AsSingle();
        }
    }
}