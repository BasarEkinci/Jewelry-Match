using System;
using _GameFolders.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI
{
    public class GamePanel : MonoBehaviour
    {
        #region Serialized Variables
        [Header("Settings")]
        [SerializeField] private Color fillImageInitialColor;
        [SerializeField] private Color fillImageMediumColor;
        [SerializeField] private Color fillImageDangerColor;
        
        [Header("References")]
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Image levelTimeFillImage;
        #endregion
        #region Private Variables
        private float _remainingTime;
        private float _levelTime;
        private int _earnedStart;
        private bool _isLevelCompleted;
        #endregion
        #region  Unity Methods

        private void OnEnable()
        {
            InitializePanelValues();
            GameEventManager.OnHourglassCollected += OnHourglassCollected;
            GameEventManager.OnTargetReached += OnTargetReached;
        }
        private void OnDisable()
        {
            GameEventManager.OnHourglassCollected -= OnHourglassCollected;
            GameEventManager.OnTargetReached -= OnTargetReached;
        }
        private void Update()
        {
            if (_isLevelCompleted)
            {
                return;   
            }
            if (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
                levelTimeFillImage.fillAmount = _remainingTime / _levelTime;
                int minutes = Mathf.FloorToInt(_remainingTime / 60f);
                int seconds = Mathf.FloorToInt(_remainingTime % 60f);
                timeText.SetText($"{minutes:00}:{seconds:00}");
            }
            else
            {
                timeText.SetText("00:00");
                levelTimeFillImage.fillAmount = 0f;
            }
            SetFillImageColor();
        }
        #endregion
        #region Helper Methods

        private void InitializePanelValues()
        {
            _isLevelCompleted = false;
            _levelTime = GameManager.Instance.CurrentLevel.LevelTime;
            levelText.SetText("Level {0}", GameManager.Instance.CurrentLevelIndex + 1);
            _remainingTime = GameManager.Instance.CurrentLevel.LevelTime;
        }
        private void SetFillImageColor()
        {
            float value = _remainingTime;
            if (value > _levelTime / 2)
            {
                levelTimeFillImage.color = fillImageInitialColor;
                _earnedStart = 3;
            }
            else if (value < _levelTime /2)
            {
                levelTimeFillImage.color = fillImageMediumColor;
                _earnedStart = 2;
            }
            else if (value < _levelTime / 4)
            {
                levelTimeFillImage.color = fillImageDangerColor;
                _earnedStart = 1;
            }
        }

        #endregion
        #region Event Handlers
        private void OnTargetReached()
        {
            GameEventManager.InvokeLevelCompleted(_earnedStart);
            _isLevelCompleted = true;
        }
        private void OnHourglassCollected(float amount)
        {
            _remainingTime += amount;
            if (_remainingTime > _levelTime)
            {
                _remainingTime = _levelTime;
            }
            timeText.transform.DOScale(transform.localScale * 1.1f,0.2f).SetLoops(2,LoopType.Yoyo);
        }
        #endregion
    }
}