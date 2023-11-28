using Playground.Common.UI;
using Playground.Events;
using Playground.Game;
using Playground.Services.Audio;
using Playground.Services.Event;
using Playground.Services.Save;
using Playground.Services.UI;
using UnityEngine;
using Zenject;

namespace Playground.Infrastructure.SceneController
{
    public class GameSceneController : MonoBehaviour
    {
        #region Variables

        private AudioService _audioService;
        private EventBus _eventBus;
        private GameScreenService _gameScreenService;
        private GameDataService _gameDataService;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(EventBus eventBus, AudioService audioService, GameScreenService gameScreenService,
            GameDataService gameDataService)
        {
            _eventBus = eventBus;
            _audioService = audioService;
            _gameScreenService = gameScreenService;
            _gameDataService = gameDataService;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _gameDataService.Data.User.LevelIndex++;

            _gameScreenService.ShowScreen();
            _audioService.PlayMusic();

            // This is for example only
            _eventBus.Fire(new LevelStartEvent
            {
                levelName = "Test",
                countSmth = 15,
            });
        }

        private void OnDestroy()
        {
            _audioService.StopMusic();
            _gameScreenService.HideScreen();
        }

        #endregion
    }
}