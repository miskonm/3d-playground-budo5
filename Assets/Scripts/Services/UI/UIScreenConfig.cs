using System.Collections.Generic;
using UnityEngine;

namespace Playground.Services.UI
{
    [CreateAssetMenu(fileName = nameof(UIScreenConfig), menuName = "Playground/UI/UI Screen Config")]
    public class UIScreenConfig : ScriptableObject
    {
        [SerializeField] private List<BaseUIScreen> _screen;

        public List<BaseUIScreen> Screen => _screen;
    }
}