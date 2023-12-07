namespace Playground.Services.GameManagementNew.StateMachine
{
    public abstract class BaseState : IStateMachineSettable, IBaseState
    {
        protected GameStateMachine StateMachine { get; private set; }

        public virtual void Exit() { }

        void IStateMachineSettable.SetStateMachine(GameStateMachine stateMachine) =>
            StateMachine = stateMachine;
    }
}