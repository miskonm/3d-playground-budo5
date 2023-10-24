using Playground.Services.Game;
using UnityEngine;

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
            Debug.LogError("BootstrapService Bootstrap");

            _gameService.TransitionToGame();
        }

        #endregion
    }
}