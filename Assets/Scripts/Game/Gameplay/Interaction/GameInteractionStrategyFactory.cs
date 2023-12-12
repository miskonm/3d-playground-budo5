using System;
using Playground.DI;

namespace Playground.Game.Gameplay.Interaction
{
    public class GameInteractionStrategyFactory
    {
        #region Variables

        private readonly IInstantiator _instantiator;

        #endregion

        #region Setup/Teardown

        public GameInteractionStrategyFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        #endregion

        #region Public methods

        public GameInteractionStrategy Create(LevelType type)
        {
            return _instantiator.Instantiate(GetStrategyType(type)) as GameInteractionStrategy;
        }

        #endregion

        #region Private methods

        private Type GetStrategyType(LevelType type)
        {
            return type switch
            {
                LevelType.Default => typeof(DefaultGameInteractionStrategy),
                LevelType.Sleepy => typeof(SleepyGameInteractionStrategy),
                _ => typeof(DefaultGameInteractionStrategy),
            };
        }

        #endregion
    }
}