using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Playground.Services.UI.Editor
{
    [CustomEditor(typeof(UIScreenConfig))]
    public class UIScreenConfigEditor : UnityEditor.Editor
    {
        #region Variables

        private ScreenInfoContainer _container;
        private SerializedObject _containerSerializedObject;
        private SerializedProperty _screenPathsSerializedProperty;

        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            _screenPathsSerializedProperty = serializedObject.FindProperty("_screenPaths");

            ValidateContainer();
            ApplyPaths();
        }

        #endregion

        #region Public methods

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((UIScreenConfig)target),
                typeof(UIScreenConfig), false);
            GUI.enabled = true;

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(GetInfoProperty());
            bool isChanged = false;
            if (EditorGUI.EndChangeCheck())
            {
                ApplyPaths();
                isChanged = true;
            }

            UpdateInfos();

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_screenPathsSerializedProperty);
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            if (isChanged)
            {
                EditorUtility.SetDirty(_container);
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
        }

        #endregion

        #region Private methods

        private void ApplyPaths()
        {
            List<string> paths = new();
            SerializedProperty screenInfoSerializedProperty = GetInfoProperty();
            for (int i = 0; i < screenInfoSerializedProperty.arraySize; i++)
            {
                SerializedProperty infoProperty = screenInfoSerializedProperty.GetArrayElementAtIndex(i);
                SerializedProperty prefabProperty = infoProperty.FindPropertyRelative("_prefab");
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
        }

        private SerializedProperty GetInfoProperty()
        {
            return _containerSerializedObject.FindProperty("Infos");
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
            SerializedProperty screenInfoSerializedProperty = GetInfoProperty();
            for (int i = 0; i < screenInfoSerializedProperty.arraySize; i++)
            {
                SerializedProperty infoProperty = screenInfoSerializedProperty.GetArrayElementAtIndex(i);
                SerializedProperty prefabProperty = infoProperty.FindPropertyRelative("_prefab");
                SerializedProperty nameProperty = infoProperty.FindPropertyRelative("_name");

                Object screen = prefabProperty.objectReferenceValue;
                nameProperty.stringValue = screen == null ? string.Empty : screen.name;
            }
        }

        private void ValidateContainer()
        {
            if (_container == null || _containerSerializedObject == null)
            {
                _container = CreateInstance<ScreenInfoContainer>();
                UIScreenConfig config = (UIScreenConfig)target;
                _container.Init(config.ScreenPath);
                _containerSerializedObject = new SerializedObject(_container);
            }
        }

        #endregion
    }

    public class ScreenInfoContainer : ScriptableObject
    {
        #region Public Nested Types

        [Serializable]
        public class ScreenInfo
        {
            #region Variables

            [HideInInspector]
            [SerializeField] private string _name;
            [SerializeField] private BaseUIScreen _prefab;

            #endregion

            #region Setup/Teardown

            public ScreenInfo(BaseUIScreen prefab)
            {
                _prefab = prefab;
                _name = _prefab.name;
            }

            #endregion
        }

        #endregion

        #region Variables

        public List<ScreenInfo> Infos;

        #endregion

        #region Public methods

        public void Init(List<string> screenPaths)
        {
            Infos = new List<ScreenInfo>();
            if (screenPaths == null)
            {
                return;
            }

            for (int i = 0; i < screenPaths.Count; i++)
            {
                string screenPath = screenPaths[i];
                if (string.IsNullOrEmpty(screenPath))
                {
                    Debug.LogError($"[{nameof(UIScreenConfig)}] There is empty path in config on index '{i}'");
                    continue;
                }

                string validAssetPath = GetValidAssetPath(screenPath);
                BaseUIScreen prefab = AssetDatabase.LoadAssetAtPath<BaseUIScreen>(validAssetPath);
                if (prefab == null)
                {
                    Debug.LogError($"[{nameof(UIScreenConfig)}] No prefab at path '{validAssetPath}' for index '{i}'");
                    continue;
                }

                Infos.Add(new ScreenInfo(prefab));
            }
        }

        #endregion

        #region Private methods

        private string GetValidAssetPath(string customScreenPath)
        {
            return $"Assets/Resources/{customScreenPath}.prefab";
        }

        #endregion
    }
}