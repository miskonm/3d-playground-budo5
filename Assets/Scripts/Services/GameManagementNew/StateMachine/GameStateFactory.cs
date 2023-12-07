using Playground.DI;

namespace Playground.Services.GameManagementNew.StateMachine
{
    public class GameStateFactory
    {
        private readonly IInstantiator _instantiator;

        public GameStateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TState Create<TState>() where TState : IBaseState =>
            _instantiator.Instantiate<TState>();
    }
}