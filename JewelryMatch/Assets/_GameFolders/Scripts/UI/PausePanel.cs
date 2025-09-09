using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Functionaries;
using _GameFolders.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI
{
    public class PausePanel : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(HandleResumeButton);
            mainMenuButton.onClick.AddListener(HandleMainMenuButton);
        }
        private void OnDisable()
        {
            resumeButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();
        }
        private void HandleMainMenuButton()
        {
            GameEventManager.InvokeGameStateChanged(GameState.GameOver);
            VibrationHelper.Vibrate(100);
        }
        private void HandleResumeButton()
        {
            GameEventManager.InvokeGamePaused(false);
            VibrationHelper.Vibrate(100);
        }
    }
}