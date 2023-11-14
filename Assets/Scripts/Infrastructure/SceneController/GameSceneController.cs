using Playground.Audio;
using Playground.Events;
using Playground.Services.Event;
using UnityEngine;
using Zenject;

namespace Playground.Infrastructure.SceneController
{
    public class GameSceneController : MonoBehaviour
    {
        #region Variables

        private AudioService _audioService;
        private EventBus _eventBus;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(EventBus eventBus, AudioService audioService)
        {
            _eventBus = eventBus;
            _audioService = audioService;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
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
        }

        #endregion
    }

    public class TestEventClass
    {
        #region Variables

        private readonly EventBus _eventBus;

        #endregion

        #region Setup/Teardown

        public TestEventClass(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        #endregion

        #region Public methods

        public void Init()
        {
            _eventBus.Subscribe<LevelStartEvent>(OnLevelStarted);
            _eventBus.Subscribe<LevelStartEvent>(OnLevelStarted2);
        }

        #endregion

        #region Private methods

        private void OnLevelStarted() { }

        private void OnLevelStarted2(LevelStartEvent args)
        {
            string argsLevelName = args.levelName;
            int argsCountSmth = args.countSmth;
        }

        #endregion
    }
}