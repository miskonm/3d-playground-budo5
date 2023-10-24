using Cysharp.Threading.Tasks;
using Playground.Services.Scene;

namespace Playground.Services.Game
{
    public class GameService
    {
        #region Variables

        private readonly SceneLoader _sceneLoader;

        #endregion

        #region Setup/Teardown

        public GameService(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        #endregion

        #region Public methods

        public void TransitionToGame()
        {
            _sceneLoader.LoadSceneAsync(SceneName.Game).Forget();
        }

        #endregion
    }
}