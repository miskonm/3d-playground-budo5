using UnityEngine;

namespace Playground.Services.UI
{
    public abstract class BaseUIScreen : MonoBehaviour
    {
        #region Public methods

        public void Hide()
        {
            OnHide();
            Destroy(gameObject);
        }

        public void Show()
        {
            OnShow();
        }

        #endregion

        #region Protected methods

        protected virtual void OnHide() { }
        protected virtual void OnShow() { }

        #endregion
    }
}