using UnityEngine;

namespace Playground.Services.UI
{
    public class UILayerProvider : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Transform _screenLayerTransform;
        [SerializeField] private Transform _popupLayerTransform;

        #endregion

        #region Public methods

        public Transform GetLayerTransform(UILayer layer)
        {
            return layer switch
            {
                UILayer.Screen => _screenLayerTransform,
                UILayer.Popup => _popupLayerTransform,
                _ => _screenLayerTransform,
            };
        }

        #endregion
    }
}