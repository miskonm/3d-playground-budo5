using Playground.Services.Event;
using UnityEditor;

namespace Playground.Services.AppState
{
    public class EditorAppStateProvider : AppStateProvider
    {
        #region Setup/Teardown

        public EditorAppStateProvider(EventBus eventBus) : base(eventBus) { }

        #endregion

        #region Protected methods

        protected override void OnBootstrapped()
        {
            base.OnBootstrapped();

#if UNITY_EDITOR
            EditorApplication.pauseStateChanged += OnPausedChanged;
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
#endif
        }

        #endregion

        #region Private methods

        private void OnPausedChanged(PauseState state)
        {
            FireChangeEvent(state == PauseState.Unpaused);
        }

        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                FireChangeEvent(false);
            }
        }

        #endregion
    }
}