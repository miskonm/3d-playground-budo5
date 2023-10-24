using Playground.Services.Game;
using Playground.Services.Scene;
using Zenject;

namespace Playground.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            SceneLoaderInstaller.Install(Container);
            GameServiceInstaller.Install(Container);
        }

        #endregion
    }
}