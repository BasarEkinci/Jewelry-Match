using _GameFolders.Scripts.Enums;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;

        public GameState CurrentState => _currentState;
        private GameState _currentState;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void ChangeState(GameState newState)
        {
            _currentState = newState;
        }
    }
}