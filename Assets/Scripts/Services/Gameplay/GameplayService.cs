using System.Collections.Generic;
using Playground.Events;
using Playground.Services.Event;

namespace Playground.Services.Gameplay
{
    public class GameplayService
    {
        #region Variables

        private readonly EventBus _eventBus;
        private readonly List<GameplaySubService> _gameplaySubServices;
        private readonly LevelBuilder _levelBuilder;

        #endregion

        #region Properties

        public LevelModel LevelModel { get; private set; }

        #endregion

        #region Setup/Teardown

        public GameplayService(List<GameplaySubService> gameplaySubServices, LevelBuilder levelBuilder,
            EventBus eventBus)
        {
            _gameplaySubServices = gameplaySubServices;
            _levelBuilder = levelBuilder;
            _eventBus = eventBus;
        }

        #endregion

        #region Public methods

        public void Dispose()
        {
            foreach (GameplaySubService gameplaySubService in _gameplaySubServices)
            {
                gameplaySubService.Dispose();
            }

            LevelModel.Dispose();
            LevelModel = null;
        }

        public void Initialize(LevelData levelData)
        {
            LevelModel = _levelBuilder.Build(levelData);

            foreach (GameplaySubService gameplaySubService in _gameplaySubServices)
            {
                gameplaySubService.SetLevelModel(LevelModel);
            }

            foreach (GameplaySubService gameplaySubService in _gameplaySubServices)
            {
                gameplaySubService.Initialize();
            }

            _eventBus.Fire(new GamePreparedEvent(LevelModel));
        }

        public void StartGame()
        {
            LevelModel.SetLevelState(LevelState.Playing);
            _eventBus.Fire(new GameStartedEvent(LevelModel));
        }

        #endregion
    }
}