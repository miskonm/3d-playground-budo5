using System.Collections.Generic;

namespace Playground.Extensions
{
    public static class CollectionExtensions
    {
        #region Public methods

        public static T Random<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
            {
                return default;
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T Random<T>(this List<T> array)
        {
            if (array == null || array.Count == 0)
            {
                return default;
            }

            return array[UnityEngine.Random.Range(0, array.Count)];
        }

        #endregion
    }
}