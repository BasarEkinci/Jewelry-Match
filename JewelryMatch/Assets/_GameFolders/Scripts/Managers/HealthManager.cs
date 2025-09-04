using System;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Extensions;
using _GameFolders.Scripts.Functionaries;
using TMPro;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class HealthManager : MonoSingleton<HealthManager>
    {
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text healthTimeText;
        public int HealthAmount => _heartTimer.Current;
        
        private RegenTimer _heartTimer;
        private int _currentHealth;
        private const float RegenCooldown = 1800f; 
        private const int MaxHealth = 5;

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += HandleGameStateChange;
        }
        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandleGameStateChange;
        }

        private void Start()
        {
            if (!GameDatabase.HasKey(Constants.CurrentHealthAmountKey))
                _currentHealth = GameDatabase.LoadData(Constants.CurrentHealthAmountKey, 5);
            else
                _currentHealth = GameDatabase.LoadData<int>(Constants.CurrentHealthAmountKey);
            
            string lastExit = GameDatabase.HasKey(Constants.LastExitTimeKey)
                ? GameDatabase.LoadData<string>(Constants.LastExitTimeKey)
                : "";

            _heartTimer = new RegenTimer(_currentHealth,MaxHealth, RegenCooldown);
            _heartTimer.Load(_currentHealth,lastExit);

            UpdateUI();
        }

        private void Update()
        {
            if (_heartTimer.Tick())
                UpdateUI();

            if (_heartTimer.Current >= 5)
            {
                healthTimeText.SetText("Full");
            }
            else
            {
                TimeSpan remain = _heartTimer.GetRemainingTime();
                healthTimeText.SetText("{0:00}:{1:00}", remain.Minutes, remain.Seconds);
            }
        }

        private void HandleGameStateChange(GameState state)
        {
            if (state == GameState.GameOver)
            {
                _heartTimer.UseOne();
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            healthText.SetText(_heartTimer.Current.ToString());
        }

        private void OnApplicationQuit()
        {
            if (GameDatabase.HasKey(Constants.LastExitTimeKey))
                GameDatabase.SaveData(Constants.LastExitTimeKey, DateTime.Now.ToString());

            if (GameDatabase.HasKey(Constants.CurrentHealthAmountKey))
                GameDatabase.SaveData(Constants.CurrentHealthAmountKey, _heartTimer.Current);
        }
    }
}