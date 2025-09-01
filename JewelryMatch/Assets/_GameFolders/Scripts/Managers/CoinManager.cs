using _GameFolders.Scripts.Extensions;
using TMPro;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class CoinManager : MonoSingleton<CoinManager>
    {
        [SerializeField] private TMP_Text coinText;

        private int _currentCoin;
        
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
            coinText.text = _currentCoin.ToString();
        }
    }
}