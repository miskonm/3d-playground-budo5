using Cysharp.Threading.Tasks;
using Playground.Services.Log;
using UnityEngine.SceneManagement;

namespace Playground.Services.Scene
{
    public class SceneLoader
    {
        #region Public methods

        public SceneLoader()
        {
            this.LogError($"ctor");
        }

        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName);
            await UniTask.Yield();
        }

        #endregion
    }
}