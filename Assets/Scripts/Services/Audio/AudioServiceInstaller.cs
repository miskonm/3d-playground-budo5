

using Playground.DI;

namespace Playground.Services.Audio
{
    public class AudioServiceInstaller : Installer<AudioServiceInstaller>
    {
        protected override void InstallBindings()
        {
            Container.Bind<AudioService>();
            // Container.Bind<AudioService>().AsSingle().WithArguments(Container.DefaultParent);
        }
    }
}