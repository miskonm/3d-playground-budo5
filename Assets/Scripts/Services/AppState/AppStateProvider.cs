using Playground.Services.Event;

namespace Playground.Services.AppState
{
    public abstract class AppStateProvider
    {
        #region Variables

        private readonly EventBus _eventBus;

        #endregion

        #region Setup/Teardown

        protected AppStateProvider(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        #endregion

        #region Public methods

        public void Bootstrap()
        {
            OnBootstrapped();
        }

        #endregion

        #region Protected methods

        protected void FireChangeEvent(bool hasFocus)
        {
            _eventBus.Fire(new AppStateChangedEvent
            {
                HasFocus = hasFocus,
            });
        }

        protected virtual void OnBootstrapped() { }

        #endregion
    }
}