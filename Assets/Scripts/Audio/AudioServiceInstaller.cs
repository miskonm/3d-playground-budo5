using Zenject;

namespace Playground.Audio
{
    public class AudioServiceInstaller : Installer<AudioServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioService>().AsSingle().WithArguments(Container.DefaultParent);
        }
    }
}