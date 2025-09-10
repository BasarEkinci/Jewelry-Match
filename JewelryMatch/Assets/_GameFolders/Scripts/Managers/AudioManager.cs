using _GameFolders.Scripts.Data.UnityObjects;
using _GameFolders.Scripts.Extensions;
using _GameFolders.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [Header("Audio Data")] [SerializeField]
        private AudioDataSo audioData;

        [Header("Components")] [SerializeField]
        private AudioSource musicSource;

        [SerializeField] private AudioSource sfxSource;

        public AudioDataSo AudioData => audioData;

        private bool _isMusicOn;
        private bool _initialized = true;

        private async void Start()
        {
            await UniTask.DelayFrame(1);
            _initialized = false;
        }

        private void OnEnable()
        {
            _isMusicOn = SettingsButtonManager.Instance.IsMusicOn;
            if (_isMusicOn) musicSource.Play();
            else musicSource.Pause();
        }

        public void PlaySfx(AudioClip clip)
        {
            sfxSource.mute = !SettingsButtonManager.Instance.IsSfxOn;
            if (clip == null || _initialized) return;
            sfxSource.PlayOneShot(clip);
        }
    }
}