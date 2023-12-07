using System;

namespace Playground.Services.GameManagementNew.StateMachine
{
    public class GameStateMachine
    {
        private readonly GameStateFactory _stateFactory;

        private IBaseState _currentState;

        public Type CurrentStateType => _currentState?.GetType();

        public GameStateMachine(GameStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Enter<TState>() where TState : class, IState
        {
            var newState = ChangeState<TState>();
            newState?.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload = default) where TState : class, IPayloadState<TPayload>
        {
            var newState = ChangeState<TState>();
            newState?.Enter(payload);
        }

        public bool IsIn<TState>() where TState : class, IBaseState =>
            _currentState is TState;

        private TState ChangeState<TState>() where TState : class, IBaseState
        {
            _currentState?.Exit();
            var newState = _stateFactory.Create<TState>();
            ((IStateMachineSettable)newState).SetStateMachine(this);
            _currentState = newState;

            return newState;
        }
    }
}