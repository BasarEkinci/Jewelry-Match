using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;
using TMPro;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class HealthManager : MonoSingleton<HealthManager>
    {
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text healthTimeText;
        
        public int HealthAmount => _healthAmount;
        
        private float _healthRegenerationTime = 1800; // 30 minutes
        private int _healthAmount = 4;
        private GameState _gameState;

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += HandleGameStateChange;
            healthText.SetText("{0}", _healthAmount);
        }
        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandleGameStateChange;
        }
        private void Update()
        {
            if (_healthAmount == 5) return;
            if (_healthAmount < 5)
            {
                _healthRegenerationTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(_healthRegenerationTime / 60f);
                int seconds = Mathf.FloorToInt(_healthRegenerationTime % 60f);
                healthTimeText.SetText("{0:00}:{1:00}", minutes, seconds);
                if (_healthRegenerationTime <= 0)
                {
                    _healthAmount++;
                    healthText.text = _healthAmount.ToString();
                    _healthRegenerationTime = 900f;
                }
            }
            else
            {
                healthTimeText.SetText("Full");
            }
        }

        private void HandleGameStateChange(GameState state)
        {
            if (state == GameState.GameOver)
            {
                if (_healthAmount > 0)
                {
                    _healthAmount--;
                    healthText.text = _healthAmount.ToString();
                }
            }
        }
    }
}