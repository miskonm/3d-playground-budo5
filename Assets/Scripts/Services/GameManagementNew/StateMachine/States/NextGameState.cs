namespace Playground.Services.GameManagementNew.StateMachine.States
{
    public class NextGameState : BaseState, IState
    {
        #region IState

        public void Enter()
        {
            // TODO: Some custom logic. Mark next lvl or smt

            StateMachine.Enter<GameState, GameState.Model>();
        }

        #endregion
    }
}