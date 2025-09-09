using _GameFolders.Scripts.Enums;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class PanelManager : MonoBehaviour
    {
        [Header("Parent Panels")]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject mainMenuPanels;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject settingsPanel;
        
        [Header("Child Panels")]
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject slotsFullPanel;

        private void Start()
        {
            SetInitialPanelState();
        }

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += HandlePanelChange;
            GameEventManager.OnGamePaused += isPaused => pausePanel.SetActive(isPaused);
        }

        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandlePanelChange;
            GameEventManager.OnGamePaused -= isPaused => pausePanel.SetActive(isPaused);
        }

        private void HandlePanelChange(GameState state)
        {
            switch (state)
            {
                case GameState.GameOver:
                case GameState.MainMenu:
                    gamePanel.SetActive(false);
                    winPanel.SetActive(false);
                    slotsFullPanel.SetActive(false);
                    mainMenuPanels.SetActive(true);
                    pausePanel.SetActive(false);
                    break;
                case GameState.GameStart:
                    mainMenuPanels.SetActive(false);
                    gamePanel.SetActive(true);
                    break;
                case GameState.GameWin:
                    winPanel.SetActive(true);
                    break;
                case GameState.NoMoreSpaces:
                    slotsFullPanel.SetActive(true);
                    break;
            }
        }

        private void SetInitialPanelState()
        {
            CloseSettingsPanel();
            mainMenuPanels.SetActive(true);
            gamePanel.SetActive(false);
            winPanel.SetActive(false);
            slotsFullPanel.SetActive(false);
        }

        public void OpenSettingsPanel()
        {
            settingsPanel.SetActive(true);
            settingsPanel.transform.DOScale(Vector3.one,0.1f).SetEase(Ease.OutBack);
        }
        
        public void CloseSettingsPanel()
        {
            settingsPanel.transform.DOScale(Vector3.zero,0.1f).SetEase(Ease.InBack).OnComplete(() =>
            {
                settingsPanel.SetActive(false);
            });
        }
    }
}