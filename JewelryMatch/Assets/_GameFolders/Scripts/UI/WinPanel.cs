using System.Collections.Generic;
using _GameFolders.Scripts.Managers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private List<Image> winStars;
        [SerializeField] private Button continueButton;
        private readonly Color _starEarnedColor = Color.white;
        private readonly Color _starDefaultColor = Color.black;
        
        private Sequence _sequence;
        
        private void Start()
        {
            continueButton.transform.DOScale(Vector3.zero, 0.1f);
            foreach (var star in winStars)
            {
                star.color = _starDefaultColor;
            }
        }

        private void OnEnable()
        {
            _sequence = DOTween.Sequence();
            GameEventManager.OnLevelCompleted += HandleLevelCompleted;
        }
        
        private void OnDisable()
        {
            GameEventManager.OnLevelCompleted -= HandleLevelCompleted;
        }
        
        private void HandleLevelCompleted(int earnedStars)
        {
            HandleEarnedStar(earnedStars).Forget();
        }

        private async UniTaskVoid HandleEarnedStar(int earnedStars)
        {
            await UniTask.Delay(500);
            for (int i = 0; i < earnedStars; i++)
            {
                _sequence.Join(winStars[i].transform.DOScale(transform.localScale * 1.1f,0.3f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.OutBack));
                winStars[i].color = _starEarnedColor;
                await UniTask.Delay(250);
            }   
            _sequence.Join(continueButton.transform.DOScale(Vector3.one,0.5f).SetEase(Ease.OutBack));
        }
    }
}