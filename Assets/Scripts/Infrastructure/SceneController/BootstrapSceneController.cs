using Playground.Services.Bootstrap;
using Playground.Services.UI;
using UnityEngine;
using Zenject;

namespace Playground.Infrastructure.SceneController
{
    public class BootstrapSceneController : MonoBehaviour
    {
        #region Variables

        private BootstrapService _bootstrapService;
        private UIService _uiService;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(BootstrapService bootstrapService, UIService uiService)
        {
            _bootstrapService = bootstrapService;
            _uiService = uiService;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _bootstrapService.Bootstrap();
            _uiService.Bootstrap();
        }

        #endregion
    }
}