using _GameFolders.Scripts.Extensions;
using _GameFolders.Scripts.Functionaries;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts.UI
{
    public class SettingsButton : MonoBehaviour
    { 
        [SerializeField] private GameObject settingsPanel;
        
        [Header("Images")]
        [SerializeField] private GameObject musicCloseImage;
        [SerializeField] private GameObject sfxCloseImage;
        [SerializeField] private GameObject vibrationCloseImage;

        private bool _isMusicOn;
        private bool _isVibrationOn;
        private bool _isVfxOn;

        private void Start()
        {
            _isMusicOn = GameDatabase.LoadData<bool>(Constants.IsMusicOnKey);
            _isVibrationOn = GameDatabase.LoadData<bool>(Constants.IsVibrationOnKey);
            _isVfxOn = GameDatabase.LoadData<bool>(Constants.IsSfxOnKey);
            settingsPanel.transform.localScale = Vector3.zero;
            settingsPanel.SetActive(false);
        }

        private void OnEnable()
        {
            musicCloseImage.SetActive(!_isMusicOn);
            vibrationCloseImage.SetActive(!_isVibrationOn);
            sfxCloseImage.SetActive(!_isVfxOn);
        }
        
        public void HandleMusicButton()
        {
            _isMusicOn = !_isMusicOn;
            GameDatabase.SaveData(Constants.IsMusicOnKey, _isMusicOn);
            musicCloseImage.SetActive(!_isMusicOn);
            VibrationHelper.Vibrate(100);
        }
        public void HandleVibrationButton()
        {
            _isVibrationOn = !_isVibrationOn;
            GameDatabase.SaveData(Constants.IsVibrationOnKey, _isVibrationOn);
            vibrationCloseImage.SetActive(!_isVibrationOn);
            VibrationHelper.Vibrate(100);
        }
        public void HandleSfxButton()
        {
            _isVfxOn = !_isVfxOn;
            GameDatabase.SaveData(Constants.IsSfxOnKey, _isVfxOn);
            sfxCloseImage.SetActive(!_isVfxOn);
            VibrationHelper.Vibrate(100);
        }
        
        public void OpenSettingsPanel()
        {
            settingsPanel.SetActive(true);
            settingsPanel.transform.DOScale(Vector3.one,0.1f).SetEase(Ease.OutBack);
        }

        public void CloseSettingsPanel()
        {
            settingsPanel.transform.DOScale(Vector3.zero,0.1f).SetEase(Ease.InBack).OnComplete(()=>
                settingsPanel.SetActive(false));
        }
    }
}
