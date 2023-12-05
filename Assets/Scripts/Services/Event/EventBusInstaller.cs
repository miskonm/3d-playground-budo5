

using Playground.DI;

namespace Playground.Services.Event
{
    public class EventBusInstaller : Installer<EventBusInstaller>
    {
        #region Public methods

        protected override void InstallBindings()
        {
            Container.Bind<EventBus>();
        }

        #endregion
    }
}