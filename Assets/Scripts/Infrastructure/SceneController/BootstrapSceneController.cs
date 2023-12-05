using Playground.DI;
using Playground.Services.AppState;
using Playground.Services.Bootstrap;
using Playground.Services.Save;
using Playground.Services.UI;
using UnityEngine;

namespace Playground.Infrastructure.SceneController
{
    public class BootstrapSceneController : MonoBehaviour
    {
        #region Variables

        private BootstrapService _bootstrapService;
        private UIService _uiService;
        private GameDataService _gameDataService;
        private AppStateService _appStateService;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(BootstrapService bootstrapService, UIService uiService, GameDataService gameDataService,
            AppStateService appStateService)
        {
            _bootstrapService = bootstrapService;
            _uiService = uiService;
            _gameDataService = gameDataService;
            _appStateService = appStateService;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _gameDataService.Bootstrap();
            
            _bootstrapService.Bootstrap();
            _uiService.Bootstrap();
            _appStateService.Bootstrap();
        }

        #endregion
    }
}