using System;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Action<GameState> OnGameStateChanged;
        private GameState _currentState;
    }
}