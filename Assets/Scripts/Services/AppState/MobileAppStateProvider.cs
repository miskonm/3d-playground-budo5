using Playground.Services.Event;
using UnityEngine;

namespace Playground.Services.AppState
{
    public class MobileAppStateProvider : AppStateProvider
    {
        #region Setup/Teardown

        public MobileAppStateProvider(EventBus eventBus) : base(eventBus) { }

        #endregion

        #region Protected methods

        protected override void OnBootstrapped()
        {
            base.OnBootstrapped();

            Application.focusChanged += FireChangeEvent;
        }

        #endregion
    }
}