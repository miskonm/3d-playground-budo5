using Playground.Services.Gameplay;

namespace Playground.Game.Gameplay.Interaction
{
    public class GameInteractionService : GameplaySubService
    {
        #region Variables

        private readonly GameInteractionStrategyFactory _factory;

        private GameInteractionStrategy _strategy;

        #endregion

        #region Setup/Teardown

        public GameInteractionService(GameInteractionStrategyFactory factory)
        {
            _factory = factory;
        }

        #endregion

        #region Public methods

        public void FlaskSelected(object flask)
        {
            _strategy.FlaskSelected(flask);
        }

        public void MoveTo(object fromFlask, object toFlask)
        {
            _strategy.MoveTo(fromFlask, toFlask);
        }

        #endregion

        #region Protected methods

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _strategy = _factory.Create(LevelModel.LevelType);
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _strategy = null;
        }

        #endregion
    }
}