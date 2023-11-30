using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Playground.Services.Log
{
    public static class GameLogger
    {
        #region Public methods

        [Conditional("UNITY_EDITOR")]
        public static void Log(object message, Object context = null)
        {
            Debug.Log(GetFormattedMessage(message), context);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogError(object message, Object context = null)
        {
            InternalLogError(null, message, context);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogError(this object obj, object message, Object context = null)
        {
            InternalLogError(obj.GetType(), message, context);
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object message, Object context = null)
        {
            Debug.LogWarning(GetFormattedMessage(message), context);
        }

        #endregion

        #region Private methods

        private static string GetFormattedMessage(object message, Type type = null)
        {
            string prefix = string.Empty;
            if (type != null)
            {
                prefix = $"[{type.Name}] ";
            }
            
            return $"{prefix}[{Time.frameCount}] {message}";
        }

        private static void InternalLogError(Type type, object message, Object context = null)
        {
            Debug.LogError(GetFormattedMessage(message, type), context);
        }

        #endregion
    }
}