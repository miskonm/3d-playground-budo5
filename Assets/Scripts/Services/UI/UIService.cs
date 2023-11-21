using UnityEngine;

namespace Playground.Services.UI
{
    public class UIService
    {
        #region Variables

        private readonly UILayerProvider _layerProvider;
        private readonly UIScreenProvider _screenProvider;

        #endregion

        #region Setup/Teardown

        public UIService(UILayerProvider layerProvider, UIScreenProvider screenProvider)
        {
            _layerProvider = layerProvider;
            _screenProvider = screenProvider;
        }

        #endregion

        #region Public methods

        public void Bootstrap()
        {
            _screenProvider.Bootstrap();
        }

        public TScreen ShowScreen<TScreen>(UILayer layer = UILayer.Screen) where TScreen : UIScreen
        {
            return ShowScreenInternal<TScreen, UIScreenModel>(UIScreenModel.Empty, layer);
        }

        public TScreen ShowScreen<TScreen, TModel>(TModel model, UILayer layer = UILayer.Screen)
            where TScreen : UIScreen<TModel>
        {
            return ShowScreenInternal<TScreen, TModel>(model, layer);
        }

        #endregion

        #region Private methods

        private TScreen ShowScreenInternal<TScreen, TModel>(TModel model, UILayer layer)
            where TScreen : UIScreen<TModel>
        {
            Transform layerTransform = _layerProvider.GetLayerTransform(layer);
            TScreen screen = _screenProvider.GetScreen<TScreen>(layerTransform);
            screen.Initialize(model);
            screen.Show();
            return screen;
        }

        #endregion
    }
}