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
        [SerializeField] private Button deleteAllDataButton;
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject gameplayObject;
        [SerializeField] private List<LevelDataSO> levels;
        public LevelDataSO CurrentLevel => levels[_currentLevelIndex];
        public int CurrentLevelIndex => _currentLevelIndex;
        
        private GameState _currentState;
        private int _currentLevelIndex;

        private void Start()
        {
            Application.targetFrameRate = 90;
            _currentState = GameState.MainMenu;
            _currentLevelIndex = GameDatabase.LoadData<int>(Constants.CurrentLevelKey);
            GameEventManager.InvokeGameStateChanged(_currentState);
        }

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += HandleGameStateChange;
            deleteAllDataButton.onClick.AddListener(DeleteAllData);
        }
        
        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandleGameStateChange;
            deleteAllDataButton.onClick.RemoveAllListeners();
        }
        
        private void DeleteAllData()
        {
            GameDatabase.DeleteAll();
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
                case GameState.GameOver:
                    if (gameplayObject.activeSelf)
                        gameplayObject.SetActive(false);
                    break;
                case GameState.GameWin:
                    _currentLevelIndex++;
                    GameDatabase.SaveData(Constants.CurrentLevelKey, _currentLevelIndex);
                    break;
            }
        }
    }
}