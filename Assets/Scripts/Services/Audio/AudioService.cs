using Playground.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace Playground.Services.Audio
{
    public class AudioService
    {
        #region Variables

        private const string ConfigPath = "Configs/Audio/AudioServiceConfig";
        private const string MusicSourcePrefabPath = "Configs/Audio/MusicSource";
        private const string SoundPrefsKey = "Audio/SoundVolume";

        private readonly Transform _rootTransform;

        private AudioServiceConfig _config;
        private MusicAudioSourceWrapper _musicAudioSource;
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

        public void PlayMusic()
        {
            if (_musicAudioSource.IsPlaying)
            {
                return;
            }

            PlayMusicInternal();
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

        public void StopMusic()
        {
            if (!_musicAudioSource.IsPlaying)
            {
                return;
            }
            
            _musicAudioSource.Stop();
        }

        #endregion

        #region Private methods

        private void CreateMusicSource()
        {
            MusicAudioSourceWrapper prefab = Resources.Load<MusicAudioSourceWrapper>(MusicSourcePrefabPath);
            _musicAudioSource = Object.Instantiate(prefab, _serviceRootTransform);
            _musicAudioSource.SetOnEnd(OnMusicEnded);
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

        private void OnMusicEnded() =>
            PlayMusicInternal();

        private void PlayMusicInternal()
        {
            AudioClip clip = _config.Musics.Random();
            _musicAudioSource.Play(clip);
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