namespace Playground.Services.GameManagementNew.StateMachine.States
{
    public class InitState : BaseState, IState
    {
        public void Enter()
        {
            StateMachine.Enter<GameState, GameState.Model>(new GameState.Model
            {
                isFirstStart = true,
            });
        }
    }
}