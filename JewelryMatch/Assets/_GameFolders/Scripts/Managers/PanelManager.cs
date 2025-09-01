using _GameFolders.Scripts.Enums;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class PanelManager : MonoBehaviour
    {
        [Header("Parent Panels")]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject mainMenuPanel;

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
        }

        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandlePanelChange;
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
                    break;
                case GameState.GameStart:
                    mainMenuPanel.SetActive(false);
                    gamePanel.SetActive(true);
                    break;
                case GameState.GameWin:
                    gamePanel.SetActive(false);
                    winPanel.SetActive(true);
                    break;
                case GameState.NoMoreSpaces:
                    gamePanel.SetActive(false);
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