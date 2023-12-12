using System;

namespace Playground.Services.Examples
{
    public class MediatorExample
    {
        #region Public Nested Types

        public class GameService
        {
            #region Public methods

            public void DoSmtOnWeatherChanged() { }

            public int GetLevelNumber()
            {
                //
                return 4;
            }

            #endregion
        }

        public class GameWeatherMediator
        {
            #region Variables

            private GameService _gameService;
            private WeatherService _weatherService;

            #endregion

            #region Public methods

            public void Init()
            {
                _weatherService.OnWeatherChanged += OnWeatherChanged;
            }

            #endregion

            #region Private methods

            private void OnWeatherChanged(string obj)
            {
                _gameService.DoSmtOnWeatherChanged();
            }

            #endregion
        }

        public class WeatherService
        {
            #region Variables

            private GameService _gameService;

            #endregion

            #region Events

            public event Action<string> OnWeatherChanged;

            #endregion

            #region Public methods

            public void ChangeLevelWeather()
            {
                int levelNumber = _gameService.GetLevelNumber();
                if (levelNumber == 2)
                {
                    ChangeWeather("Sunny");
                }
                else
                {
                    ChangeWeather("Rain");
                }
            }

            public void ChangeWeather(string weather)
            {
                OnWeatherChanged?.Invoke(weather);
            }

            #endregion
        }

        #endregion
    }
}