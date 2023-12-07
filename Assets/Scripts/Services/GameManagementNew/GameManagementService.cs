using Playground.Events;
using Playground.Services.Event;
using Playground.Services.GameManagementNew.StateMachine;
using Playground.Services.GameManagementNew.StateMachine.States;
using Playground.Services.Log;

namespace Playground.Services.GameManagementNew
{
    public class GameManagementService
    {
        #region Variables

        private readonly EventBus _eventBus;
        private readonly GameStateMachine _stateMachine;

        #endregion

        #region Setup/Teardown

        public GameManagementService(GameStateMachine stateMachine, EventBus eventBus)
        {
            _stateMachine = stateMachine;
            _eventBus = eventBus;
        }

        #endregion

        #region Public methods

        public void EnterNextLevel()
        {
            if (_stateMachine.IsIn<GameState>())
            {
                _stateMachine.Enter<NextGameState>();
            }
            else
            {
                this.LogError($"Can't enter '{nameof(NextGameState)}' from '{_stateMachine.CurrentStateType?.Name}'");
            }
        }

        public void Init()
        {
            _eventBus.Subscribe<AppInitEvent>(OnAppInited);
        }

        public void RestartLevel()
        {
            if (_stateMachine.IsIn<GameState>())
            {
                _stateMachine.Enter<RestartGameState>();
            }
            else
            {
                this.LogError(
                    $"Can't enter '{nameof(RestartGameState)}' from '{_stateMachine.CurrentStateType?.Name}'");
            }
        }

        #endregion

        #region Private methods

        private void OnAppInited()
        {
            _eventBus.Unsubscribe<AppInitEvent>(OnAppInited);
            _stateMachine.Enter<InitState>();
        }

        #endregion
    }
}