using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Events;
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
            EventManager.Publish(new GameStateEvent { State = newState });
            _currentState = newState;
        }
    }
}