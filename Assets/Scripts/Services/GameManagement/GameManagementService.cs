using Cysharp.Threading.Tasks;
using Playground.Events;
using Playground.Services.Event;
using Playground.Services.Scene;

namespace Playground.Services.GameManagement
{
    public class GameManagementService
    {
        #region Variables

        private readonly EventBus _eventBus;
        private readonly SceneLoader _sceneLoader;

        #endregion

        #region Setup/Teardown

        public GameManagementService(SceneLoader sceneLoader, EventBus eventBus)
        {
            _sceneLoader = sceneLoader;
            _eventBus = eventBus;
        }

        #endregion

        #region Public methods

        public void Init()
        {
            _eventBus.Subscribe<AppInitEvent>(OnAppInited);
        }

        #endregion

        #region Private methods

        private void OnAppInited()
        {
            _eventBus.Unsubscribe<AppInitEvent>(OnAppInited);
            TransitionToGame();
        }

        private void TransitionToGame()
        {
            _sceneLoader.LoadSceneAsync(SceneName.Game).Forget();
        }

        #endregion
    }
}