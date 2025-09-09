using _GameFolders.Scripts.Data.UnityObjects;
using _GameFolders.Scripts.Extensions;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [Header("Audio Data")]
        [SerializeField] private AudioDataSo audioData;
        
        [Header("Components")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        public AudioDataSo AudioData => audioData;
        
        private bool _isMusicOn;
        private bool _isSfxOn;
        
        private void OnEnable()
        {
            _isMusicOn = GameDatabase.LoadData<bool>(Constants.IsMusicOnKey);
            if (_isMusicOn) musicSource.Play();
            else musicSource.Pause();
        }
        
        public void PlaySfx(AudioClip clip)
        {
            sfxSource.mute = !GameDatabase.LoadData<bool>(Constants.IsSfxOnKey);
            if (clip == null || !_isSfxOn) return;
            sfxSource.PlayOneShot(clip);
        }
    }
}