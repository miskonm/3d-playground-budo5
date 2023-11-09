using UnityEngine;
using UnityEngine.Assertions;

namespace Playground.Audio
{
    public class AudioService
    {
        #region Variables

        private const string ConfigPath = "Configs/Audio/AudioServiceConfig";
        private const string MusicSourcePrefabPath = "Configs/Audio/MusicSource";
        private const string SoundPrefsKey = "Audio/SoundVolume";

        private readonly Transform _rootTransform;

        private AudioServiceConfig _config;
        private AudioSource _musicAudioSource;
        private Transform _serviceRootTransform;
        private AudioSource _soundAudioSource;

        #endregion

        #region Properties

        public float SoundVolume
        {
            get => PlayerPrefs.GetFloat(SoundPrefsKey, 1);
            private set => PlayerPrefs.SetFloat(SoundPrefsKey, value);
        }

        #endregion

        #region Setup/Teardown

        public AudioService(Transform rootTransform)
        {
            _rootTransform = rootTransform;
        }

        #endregion

        #region Public methods

        public void Init()
        {
            LoadConfig();
            CreateRootObject();
            CreateSoundSource();
            CreateMusicSource();
        }

        public void PlaySound(SoundType type)
        {
            AudioClip clip = _config.GetSound(type);
            PlaySoundClip(clip);
        }

        public void SetSoundVolume(float value)
        {
            SoundVolume = value;
            _soundAudioSource.volume = SoundVolume;
        }

        #endregion

        #region Private methods

        private void CreateMusicSource()
        {
            AudioSource prefab = Resources.Load<AudioSource>(MusicSourcePrefabPath);
            _musicAudioSource = Object.Instantiate(prefab, _serviceRootTransform);
        }

        private void CreateRootObject()
        {
            _serviceRootTransform = new GameObject($"[{nameof(AudioService)}]").transform;
            _serviceRootTransform.SetParent(_rootTransform);
        }

        private void CreateSoundSource()
        {
            GameObject go = new("SoundsSource");
            _soundAudioSource = go.AddComponent<AudioSource>();
            _soundAudioSource.volume = SoundVolume;
            go.transform.SetParent(_serviceRootTransform);
        }

        private void LoadConfig()
        {
            Debug.LogError("LoadConfig");
            _config = Resources.Load<AudioServiceConfig>(ConfigPath);
            Assert.IsNotNull(_config, $"{nameof(AudioService)}: {nameof(AudioServiceConfig)} is null " +
                                      $"on path '{ConfigPath}'");
        }

        private void PlaySoundClip(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            _soundAudioSource.PlayOneShot(clip);
        }

        #endregion
    }
}