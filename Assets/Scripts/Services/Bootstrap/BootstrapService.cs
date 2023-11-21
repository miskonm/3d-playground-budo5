using Cysharp.Threading.Tasks;
using Playground.Events;
using Playground.Services.Audio;
using Playground.Services.Event;
using Playground.Services.GameManagement;

namespace Playground.Services.Bootstrap
{
    public class BootstrapService
    {
        #region Variables

        private readonly EventBus _eventBus;
        private readonly GameManagementService _gameManagementService;
        private readonly AudioService _audioService;

        #endregion

        #region Setup/Teardown

        public BootstrapService(EventBus eventBus, GameManagementService gameManagementService, AudioService audioService)
        {
            _eventBus = eventBus;
            _gameManagementService = gameManagementService;
            _audioService = audioService;
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
            _gameManagementService.Init();
            _audioService.Init();
            
            await UniTask.Delay(2 * 1000);

            _eventBus.Fire<AppInitEvent>();
        }

        #endregion
    }
}