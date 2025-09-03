using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI
{
    public class GamePanel : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Color fillImageInitialColor;
        [SerializeField] private Color fillImageMediumColor;
        [SerializeField] private Color fillImageDangerColor;
        
        [Header("References")]
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Image levelTimeFillImage;
        
        private float _remainingTime;
        private int _earnedStart;
        private GameState _gameState;
        private float _levelTime => GameManager.Instance.CurrentLevel.LevelTime;
        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged -= state => _gameState = state;
            GameEventManager.OnHourglassCollected += OnHourglassCollected;
            GameEventManager.OnTargetReached += () => GameEventManager.InvokeLevelCompleted(_earnedStart);
            levelText.SetText("Level {0}", GameManager.Instance.CurrentLevelIndex + 1);
            _remainingTime = GameManager.Instance.CurrentLevel.LevelTime;
        }
        
        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= state => _gameState = state;
            GameEventManager.OnHourglassCollected -= OnHourglassCollected;
            GameEventManager.OnTargetReached -= () => GameEventManager.InvokeLevelCompleted(_earnedStart);
        }
        private void Update()
        {
            if (_gameState != GameState.GameStart)
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

        private void SetFillImageColor()
        {
            float value = _remainingTime;
            if (value > _remainingTime / 2)
            {
                levelTimeFillImage.color = fillImageInitialColor;
                _earnedStart = 3;
            }
            else if (value < _remainingTime / 2)
            {
                levelTimeFillImage.color = fillImageMediumColor;
                _earnedStart = 2;
            }
            else if (value < _remainingTime / 4)
            {
                levelTimeFillImage.color = fillImageDangerColor;
                _earnedStart = 1;
            }
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
    }
}