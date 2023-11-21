namespace Playground.Services.UI
{
    public abstract class UIScreen : UIScreen<UIScreenModel> { }

    public abstract class UIScreen<TModel> : BaseUIScreen
    {
        #region Properties

        protected TModel ScreenModel { get; private set; }

        #endregion

        #region Public methods

        public void Initialize(TModel screenModel)
        {
            ScreenModel = screenModel;
            OnInitialize();
        }

        #endregion

        #region Protected methods

        protected virtual void OnInitialize() { }

        #endregion
    }
}