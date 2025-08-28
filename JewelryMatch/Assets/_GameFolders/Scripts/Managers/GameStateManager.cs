using System;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;

namespace _GameFolders.Scripts.Managers
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        public static GameStateManager Instance;
        
        public Action<GameState> OnGameStateChanged;
        
        public GameState CurrentState => _currentState;
        private GameState _currentState;

        private void OnEnable()
        {
            OnGameStateChanged += state => _currentState = state;
        }

        private void OnDisable()
        {
            OnGameStateChanged -= state => _currentState = state;
        }
    }
}