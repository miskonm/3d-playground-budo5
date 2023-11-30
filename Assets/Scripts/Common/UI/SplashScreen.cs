using Playground.Services.Log;
using UnityEngine;

namespace Playground.Common.UI
{
    public class SplashScreen : MonoBehaviour
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