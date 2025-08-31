using _GameFolders.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.Functionaries
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
        private float _fillAmount;
        private float _levelTime => GameManager.Instance.CurrentLevel.LevelTime;
        private void OnEnable()
        {
            levelText.SetText("Level {0}", GameManager.Instance.CurrentLevelIndex + 1);
            _remainingTime = GameManager.Instance.CurrentLevel.LevelTime;
            _fillAmount = 1;
        }
        private void Update()
        {
            if (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
                levelTimeFillImage.fillAmount = _remainingTime / _levelTime;
                int minutes = Mathf.FloorToInt(_remainingTime / 60f);
                int seconds = Mathf.FloorToInt(_remainingTime % 60f);
                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timeText.text = "00:00";
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
            }
            else if (value < _remainingTime / 2)
            {
                levelTimeFillImage.color = fillImageMediumColor;    
            }
            else if (value < _remainingTime / 4)
            {
                levelTimeFillImage.color = fillImageDangerColor;
            }
        }
    }
}