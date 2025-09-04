using System;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;
using _GameFolders.Scripts.Functionaries;
using _GameFolders.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI
{
    public class PausePanel : MonoBehaviour
    {
        [Header("Images")]
        [SerializeField] private GameObject musicCloseImage;
        [SerializeField] private GameObject sfxCloseImage;
        [SerializeField] private GameObject vibrationCloseImage;
        
        [Header("Buttons")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button vibrationButton;
        [SerializeField] private Button vfxButton;

        private bool _isMusicOn;
        private bool _isVibrationOn;
        private bool _isVfxOn;

        private void Start()
        {
            _isMusicOn = GameDatabase.LoadData<bool>(Constants.IsMusicOnKey);
            _isVibrationOn = GameDatabase.LoadData<bool>(Constants.IsVibrationOnKey);
            _isVfxOn = GameDatabase.LoadData<bool>(Constants.IsSfxOnKey);
        }

        private void OnEnable()
        {
            musicCloseImage.SetActive(!_isMusicOn);
            vibrationCloseImage.SetActive(!_isVibrationOn);
            sfxCloseImage.SetActive(!_isVfxOn);
            resumeButton.onClick.AddListener(HandleResumeButton);
            mainMenuButton.onClick.AddListener(HandleMainMenuButton);
            musicButton.onClick.AddListener(HandleMusicButton);
            vibrationButton.onClick.AddListener(HandleVibrationButton);
            vfxButton.onClick.AddListener(HandleSfxButton);
        }
        
        private void OnDisable()
        {
            resumeButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();
            musicButton.onClick.RemoveAllListeners();
            vibrationButton.onClick.RemoveAllListeners();
            vfxButton.onClick.RemoveAllListeners();
        }
        private void HandleMainMenuButton()
        {
            GameEventManager.InvokeGameStateChanged(GameState.MainMenu);
            VibrationHelper.Vibrate(100);
        }
        private void HandleResumeButton()
        {
            GameEventManager.InvokeGamePaused(false);
            VibrationHelper.Vibrate(100);
        }
        private void HandleMusicButton()
        {
            _isMusicOn = !_isMusicOn;
            GameDatabase.SaveData(Constants.IsMusicOnKey, _isMusicOn);
            musicCloseImage.SetActive(!_isMusicOn);
            VibrationHelper.Vibrate(100);
        }
        private void HandleVibrationButton()
        {
            _isVibrationOn = !_isVibrationOn;
            GameDatabase.SaveData(Constants.IsVibrationOnKey, _isVibrationOn);
            vibrationCloseImage.SetActive(!_isVibrationOn);
            VibrationHelper.Vibrate(100);
        }
        private void HandleSfxButton()
        {
            _isVfxOn = !_isVfxOn;
            GameDatabase.SaveData(Constants.IsSfxOnKey, _isVfxOn);
            sfxCloseImage.SetActive(!_isVfxOn);
            VibrationHelper.Vibrate(100);
        }
    }
}