using System.Collections.Generic;
using _GameFolders.Scripts.Data.UnityObjects;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject gameplayObject;
        [SerializeField] private List<LevelDataSO> levels;
        public LevelDataSO CurrentLevel => levels[_currentLevelIndex];
        public int CurrentLevelIndex => _currentLevelIndex;
        
        private GameState _currentState;
        private int _currentLevelIndex = 0;

        private void Start()
        {
            _currentState = GameState.MainMenu;
            GameEventManager.InvokeGameStateChanged(_currentState);
        }

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += HandleGameStateChange;
        }
        
        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    playButton.interactable = _currentLevelIndex < levels.Count;
                    if (gameplayObject.activeSelf)
                        gameplayObject.SetActive(false);
                    break;
                case GameState.GameStart:
                    if (!gameplayObject.activeSelf)
                        gameplayObject.SetActive(true);
                    break;
                case GameState.GameWin:
                    _currentLevelIndex++;
                    break;
            }
        }
    }
}