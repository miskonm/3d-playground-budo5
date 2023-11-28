namespace Playground.Services.AppState
{
    public class AppStateService
    {
        private readonly AppStateProvider _stateProvider;

        public AppStateService(AppStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
        }

        public void Bootstrap()
        {
            _stateProvider.Bootstrap();
        }
    }
}