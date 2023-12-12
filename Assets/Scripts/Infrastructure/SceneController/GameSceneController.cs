using Playground.DI;
using Playground.Events;
using Playground.Game;
using Playground.Services.Audio;
using Playground.Services.Event;
using Playground.Services.Save;
using UnityEngine;

namespace Playground.Infrastructure.SceneController
{
    public class GameSceneController : MonoBehaviour
    {
        #region Variables

        private AudioService _audioService;
        private EventBus _eventBus;
        private GameDataService _gameDataService;
        private GameScreenService _gameScreenService;

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
            // _gameDataService.Data.User.LevelIndex++;

            _gameScreenService.ShowScreen();
            _audioService.PlayMusic();
        }

        private void OnDestroy()
        {
            _audioService.StopMusic();
            _gameScreenService.HideScreen();
        }

        #endregion
    }
}