using Playground.Events;
using Playground.Services.Event;
using UnityEngine;
using Zenject;

namespace Playground.Infrastructure.SceneController
{
    public class GameSceneController : MonoBehaviour
    {
        #region Variables

        private EventBus _eventBus;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            // This is for example only
            _eventBus.Fire(new LevelStartEvent
            {
                levelName = "Test",
                countSmth = 15,
            });
        }

        #endregion
    }


    public class TestEventClass
    {
        private readonly EventBus _eventBus;

        public TestEventClass(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Init()
        {
            _eventBus.Subscribe<LevelStartEvent>(OnLevelStarted);
            _eventBus.Subscribe<LevelStartEvent>(OnLevelStarted2);
        }

        private void OnLevelStarted2(LevelStartEvent args)
        {
            string argsLevelName = args.levelName;
            int argsCountSmth = args.countSmth;
        }

        private void OnLevelStarted()
        {
            
        }
    }
}