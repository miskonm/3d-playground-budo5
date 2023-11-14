using UnityEngine;
using UnityEngine.UI;

namespace Playground.Extensions
{
    public static class RendererExtensions
    {
        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }
}