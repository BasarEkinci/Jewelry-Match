using _GameFolders.Scripts.Enums;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class PanelManager : MonoBehaviour
    {
        [Header("Parent Panels")]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject pausePanel;

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
                    mainMenuPanel.SetActive(true);
                    pausePanel.SetActive(false);
                    break;
                case GameState.GameStart:
                    mainMenuPanel.SetActive(false);
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
            mainMenuPanel.SetActive(true);
            gamePanel.SetActive(false);
            winPanel.SetActive(false);
            slotsFullPanel.SetActive(false);
        }
    }
}