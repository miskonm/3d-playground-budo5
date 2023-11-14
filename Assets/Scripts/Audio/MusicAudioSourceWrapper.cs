using System;
using UnityEngine;

namespace Playground.Audio
{
    public class MusicAudioSourceWrapper : MonoBehaviour
    {
        #region Variables

        [SerializeField] private AudioSource _source;

        private Action _endClipCallback;

        #endregion

        #region Properties

        public bool IsPlaying { get; private set; }

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (!IsPlaying)
            {
                return;
            }

            if (!_source.isPlaying)
            {
                EndInternal();
            }
        }

        #endregion

        #region Public methods

        public void Play(AudioClip clip)
        {
            IsPlaying = true;
            _source.clip = clip;
            _source.Play();
        }

        public void SetOnEnd(Action endClipCallback)
        {
            _endClipCallback = endClipCallback;
        }

        public void Stop()
        {
            IsPlaying = false;
            _source.Stop();
            _source.clip = null;
        }

        #endregion

        #region Private methods

        private void EndInternal()
        {
            IsPlaying = false;
            _source.clip = null;
            _endClipCallback?.Invoke();
        }

        #endregion
    }
}