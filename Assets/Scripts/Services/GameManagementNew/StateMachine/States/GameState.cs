using Cysharp.Threading.Tasks;
using Playground.Services.Gameplay;

namespace Playground.Services.GameManagementNew.StateMachine.States
{
    public class GameState : BaseState, IPayloadState<GameState.Model>
    {
        #region Public Nested Types

        public struct Model
        {
            #region Variables

            public bool isFirstStart;
            public bool isRestart;

            #endregion
        }

        #endregion

        #region Variables

        private readonly GameplayService _gameplayService;

        #endregion

        #region Setup/Teardown

        public GameState(GameplayService gameplayService)
        {
            _gameplayService = gameplayService;
        }

        #endregion

        #region IPayloadState<Model>

        public void Enter(Model payload)
        {
            EnterAsync(payload).Forget();
        }

        public override void Exit()
        {
            base.Exit();

            _gameplayService.Dispose();
        }

        #endregion

        #region Private methods

        private async UniTaskVoid EnterAsync(Model model)
        {
            // await PreparationToShowLevelAsync();
            InitializeLevel();
            // await ShowInterstitialAsync(model);
            // await ShowRateUsAsync();
            StartGame();
        }

        private LevelData GetLevelData()
        {
            return new LevelData
            {
                id = "olol",
            };
        }

        private void InitializeLevel()
        {
            _gameplayService.Initialize(GetLevelData());
        }

        private async UniTask PreparationToShowLevelAsync()
        {
            // TODO: Show screen here
            // _gameScreen = await _uiService.Show<GameScreen>(); 
        }

        private async UniTask ShowInterstitialAsync(Model model)
        {
            if (model.isFirstStart)
            {
                return;
            }

            if (model.isRestart)
            {
                // Show restart ads
            }
            else
            {
                // Show next level ads
            }
            // TODO: Show ad here
            // await _adsService.ShowInter();
        }

        private async UniTask ShowRateUsAsync()
        {
            // TODO: Try show rate us here
        }

        private void StartGame()
        {
            _gameplayService.StartGame();
        }

        #endregion
    }
}