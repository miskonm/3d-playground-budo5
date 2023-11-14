using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playground.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioServiceConfig), menuName = "Playground/Audio/Audio Config")]
    public class AudioServiceConfig : ScriptableObject
    {
        #region Variables

        [Header("Sounds")]
        [SerializeField] private SoundInfo[] _sounds;

        [Header("Music")]
        [SerializeField] private AudioClip[] _musics;
        
        private readonly Dictionary<SoundType, AudioClip> _clipsByType = new();

        #endregion

        #region Properties

        public AudioClip[] Musics => _musics;

        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            FillDict();
        }

        private void OnValidate()
        {
            if (_sounds == null)
            {
                return;
            }

            foreach (SoundInfo info in _sounds)
            {
                info.Validate();
            }
        }

        #endregion

        #region Public methods

        public AudioClip GetSound(SoundType type)
        {
            _clipsByType.TryGetValue(type, out AudioClip clip);
            return clip;
        }

        #endregion

        #region Private methods

        private void FillDict()
        {
            _clipsByType.Clear();

            foreach (SoundInfo info in _sounds)
            {
                if (!_clipsByType.ContainsKey(info.Type))
                {
                    _clipsByType.Add(info.Type, info.Clip);
                }
                else
                {
                    Debug.LogError($"{nameof(AudioServiceConfig)}: Have several clips for type '{info.Type}'");
                }
            }
        }

        #endregion

        #region Local data

        [Serializable]
        private class SoundInfo
        {
            #region Variables

            public AudioClip Clip;
            [HideInInspector]
            public string Name;
            public SoundType Type;

            #endregion

            #region Public methods

            public void Validate()
            {
                Name = Type.ToString();
            }

            #endregion
        }

        #endregion
    }
}