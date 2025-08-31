using System;
using System.Collections.Generic;
using _GameFolders.Scripts.Data.UnityObjects;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private List<LevelDataSO> levels;
        public LevelDataSO CurrentLevel => levels[_currentLevelIndex];
        public int CurrentLevelIndex => _currentLevelIndex;
        
        private GameState _currentState;
        private int _currentLevelIndex = 0;

        private void Start()
        {
            _currentState = GameState.GameStart;
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
            if (state == GameState.GameWin)
            {
                //_currentLevelIndex++;
            }
        }
    }
}