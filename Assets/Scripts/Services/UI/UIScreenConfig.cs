using System.Collections.Generic;
using UnityEngine;

namespace Playground.Services.UI
{
    [CreateAssetMenu(fileName = nameof(UIScreenConfig), menuName = "Playground/UI/UI Screen Config")]
    public class UIScreenConfig : ScriptableObject
    {
        #region Variables

        [SerializeField] private List<string> _screenPaths;

        #endregion

        #region Properties

        public List<string> ScreenPath => _screenPaths;

        #endregion
    }
}