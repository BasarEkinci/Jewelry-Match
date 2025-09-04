using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;
using TMPro;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class CoinManager : MonoSingleton<CoinManager>
    {
        [SerializeField] private TMP_Text coinText;
        public int EarnedCoin => 5 + _earnedCoin; // Base 5 coin + collected coins in the level
        private int _currentCoin;
        private int _earnedCoin;
        
        // Earned coin event will be added later
        
        private void OnEnable()
        {
            GameEventManager.OnCoinCollected += OnCoinCollected;
            GameEventManager.OnGameStateChanged += OnGameStateChanged;
            _currentCoin = GameDatabase.LoadData<int>(Constants.CurrentCoinKey);
            coinText.text = _currentCoin.ToString(); 
        }
        
        private void OnDisable()
        {
            GameEventManager.OnCoinCollected -= OnCoinCollected;
            GameEventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.GameWin)
            {
                AddCoin(GetCurrentEarnedCoin());
                _earnedCoin = 0;
            }
        }

        private void OnCoinCollected()
        {
            _earnedCoin++;
        }

        private int GetCurrentEarnedCoin()
        {
            return 5 + _earnedCoin;
        }
        
        public void SpendCoin(int amount)
        {
            if (_currentCoin > amount)
            {
                _currentCoin -= amount;
                coinText.text = _currentCoin.ToString();   
            }
        }
        
        public void AddCoin(int amount)
        {
            _currentCoin += amount;
            GameDatabase.SaveData(Constants.CurrentCoinKey, _currentCoin);
            coinText.text = _currentCoin.ToString();
        }
    }
}