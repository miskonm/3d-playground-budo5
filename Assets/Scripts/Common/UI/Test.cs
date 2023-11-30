using Playground.Services.Log;
using UnityEngine;

namespace Playground.Common.UI
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
            this.LogError("Awake");
        }

        private void Start()
        {
            this.LogError("Start");
        }
    }
}