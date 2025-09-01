using _GameFolders.Scripts.Enums;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject mainMenuPanel;
        
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
                case GameState.MainMenu:
                    mainMenuPanel.SetActive(true);
                    gamePanel.SetActive(false);
                    break;
                case GameState.GameStart:
                    mainMenuPanel.SetActive(false);
                    gamePanel.SetActive(true);
                    break;
            }
        }
    }
}