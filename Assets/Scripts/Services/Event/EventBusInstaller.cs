using Zenject;

namespace Playground.Services.Event
{
    public class EventBusInstaller : Installer<EventBusInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle();
        }

        #endregion
    }
}