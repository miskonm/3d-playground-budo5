using Playground.Services.Bootstrap;
using Zenject;

namespace Playground.Infrastructure.Installers
{
    public class BootstrapSceneInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            BootstrapServiceInstaller.Install(Container);
        }

        #endregion
    }
}