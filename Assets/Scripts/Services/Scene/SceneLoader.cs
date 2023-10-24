using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Playground.Services.Scene
{
    public class SceneLoader
    {
        #region Public methods

        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName);
            await UniTask.Yield();
        }

        #endregion
    }
}