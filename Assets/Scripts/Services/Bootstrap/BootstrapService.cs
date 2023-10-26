using Cysharp.Threading.Tasks;
using Playground.Services.Game;

namespace Playground.Services.Bootstrap
{
    public class BootstrapService
    {
        #region Variables

        private readonly GameService _gameService;

        #endregion

        #region Setup/Teardown

        public BootstrapService(GameService gameService)
        {
            _gameService = gameService;
        }

        #endregion

        #region Public methods

        public void Bootstrap()
        {
            BootstrapAsync().Forget();
        }

        #endregion

        #region Private methods

        private async UniTask BootstrapAsync()
        {
            // await UniTask.Delay(2 * 1000);

            _gameService.TransitionToGame();
        }

        #endregion
    }
}