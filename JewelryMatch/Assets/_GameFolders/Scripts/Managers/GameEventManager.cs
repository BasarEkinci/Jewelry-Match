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
        public static event Action<float> OnHourglassCollected;
        public static event Action<int> OnLevelCompleted; // int parameter represents the earned star count
        public static event Action<bool> OnGamePaused;
        public static event Action OnCoinCollected;
        
        public static void InvokeCoinCollected()
        {
            OnCoinCollected?.Invoke();
        }
        
        public static void InvokeGamePaused(bool isPaused)
        {
            OnGamePaused?.Invoke(isPaused);
        }
        
        public static void InvokeLevelCompleted(int earnedStar)
        {
            OnLevelCompleted?.Invoke(earnedStar);
        }
        public static void InvokeHourglassCollected(float additionalTime)
        {
            OnHourglassCollected?.Invoke(additionalTime);
        }
        
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