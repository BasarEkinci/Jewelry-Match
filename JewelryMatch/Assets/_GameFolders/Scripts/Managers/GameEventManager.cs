using System;
using _GameFolders.Scripts.Enums;

namespace _GameFolders.Scripts.Managers
{
    public static class GameEventManager
    {
        public static event Action<string> OnCollectJewelry;
        public static event Action OnSpawnCompleted;
        public static event Action<GameState> OnGameStateChanged;
        public static event Action OnTargetReached;
        
        public static void InvokeTargetReached()
        {
            OnTargetReached?.Invoke();
        }
        public static void InvokeCollectJewelry(string jewelryName)
        {
            OnCollectJewelry?.Invoke(jewelryName);
        }
        
        public static void InvokeSpawnCompleted()
        {
            OnSpawnCompleted?.Invoke();
        }
        
        public static void InvokeGameStateChanged(GameState newState)
        {
            OnGameStateChanged?.Invoke(newState);
        }
    }
}