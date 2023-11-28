using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playground.Services.UI
{
    [CreateAssetMenu(fileName = nameof(UIScreenConfig), menuName = "Playground/UI/UI Screen Config")]
    public class UIScreenConfig : ScriptableObject
    {
        #region Variables

        [SerializeField] private List<string> _screenPaths;

#if UNITY_EDITOR
        [SerializeField] private List<ScreenInfo> _info;
#endif

        #endregion

        #region Properties

        public List<string> ScreenPath => _screenPaths;

        #endregion
    }

    [Serializable]
    public class ScreenInfo
    {
        #region Variables

        [HideInInspector]
        [SerializeField] public string Name;
        public BaseUIScreen Prefab;

        #endregion
    }
}