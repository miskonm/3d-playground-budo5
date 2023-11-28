using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Playground.Services.UI.Editor
{
    [CustomEditor(typeof(UIScreenConfig))]
    public class UIScreenConfigEditor : UnityEditor.Editor
    {
        #region Variables

        private SerializedProperty _screenInfoSerializedProperty;
        private SerializedProperty _screenPathsSerializedProperty;

        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            _screenPathsSerializedProperty = serializedObject.FindProperty("_screenPaths");
            _screenInfoSerializedProperty = serializedObject.FindProperty("_info");
            ApplyPaths();
        }

        #endregion

        #region Public methods

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_screenPathsSerializedProperty);
            EditorGUI.EndDisabledGroup();

            UpdateInfos();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_screenInfoSerializedProperty);
            if (EditorGUI.EndChangeCheck())
            {
                ApplyPaths();
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Private methods

        private void ApplyPaths()
        {
            List<string> paths = new();
            for (int i = 0; i < _screenInfoSerializedProperty.arraySize; i++)
            {
                SerializedProperty infoProperty = _screenInfoSerializedProperty.GetArrayElementAtIndex(i);
                SerializedProperty prefabProperty = infoProperty.FindPropertyRelative("Prefab");
                Object screen = prefabProperty.objectReferenceValue;

                if (screen == null)
                {
                    continue;
                }

                string validPath = GetValidResourcePath(screen);
                paths.Add(validPath);
            }

            _screenPathsSerializedProperty.arraySize = paths.Count;
            for (int i = 0; i < paths.Count; i++)
            {
                _screenPathsSerializedProperty.GetArrayElementAtIndex(i).stringValue = paths[i];
            }

            AssetDatabase.SaveAssets();
        }

        private string GetValidResourcePath(Object screen)
        {
            string assetPath = AssetDatabase.GetAssetPath(screen);
            assetPath = assetPath.Remove(assetPath.Length - 7, 7);
            string[] strings = assetPath.Split("/");
            List<string> validPath = new();
            for (int i = strings.Length - 1; i >= 0; i--)
            {
                string pathPart = strings[i];
                if (pathPart == "Resources")
                {
                    break;
                }

                validPath.Add(pathPart);
            }

            validPath.Reverse();
            string realPath = string.Empty;
            for (int i = 0; i < validPath.Count; i++)
            {
                realPath += validPath[i];

                if (i != validPath.Count - 1)
                {
                    realPath += "/";
                }
            }

            return realPath;
        }

        private void UpdateInfos()
        {
            for (int i = 0; i < _screenInfoSerializedProperty.arraySize; i++)
            {
                SerializedProperty infoProperty = _screenInfoSerializedProperty.GetArrayElementAtIndex(i);
                SerializedProperty prefabProperty = infoProperty.FindPropertyRelative("Prefab");
                SerializedProperty nameProperty = infoProperty.FindPropertyRelative("Name");

                Object screen = prefabProperty.objectReferenceValue;
                nameProperty.stringValue = screen == null ? string.Empty : screen.name;
            }
        }

        #endregion

        // [Serializable]
        // public class ScreenInfo
        // {
        //     [HideInInspector]
        //     [SerializeField] private string _name;
        //     public BaseUIScreen Prefab;
        // }
    }
}