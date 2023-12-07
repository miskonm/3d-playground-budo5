using Playground.Events;
using Playground.Services.Event;

namespace Playground.Services.GameManagementNew.StateMachine.States
{
    public class RestartGameState : BaseState, IState
    {
        private readonly EventBus _eventBus;

        public RestartGameState(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Enter()
        {
            _eventBus.Fire(new GameFinishedEvent(false));

            StateMachine.Enter<GameState, GameState.Model>(new GameState.Model
            {
                isRestart = true,
            });
        }
    }
}