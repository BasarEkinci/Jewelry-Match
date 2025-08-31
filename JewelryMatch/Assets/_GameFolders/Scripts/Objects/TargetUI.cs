using _GameFolders.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.Objects
{
    public class TargetUI : MonoBehaviour
    {
        [SerializeField] private Image targetImage;
        [SerializeField] private TMP_Text requiredAmountText;
        
        private string _targetID;
        private int _currentAmount;
        private void OnEnable()
        {
            GameEventManager.OnCollectJewelry += OnCollectJewelry;
        }
        
        private void OnDisable()
        {
            GameEventManager.OnCollectJewelry -= OnCollectJewelry;
        }

        private void OnCollectJewelry(string id)
        {
            if (id == _targetID)
            {
                _currentAmount--;
                requiredAmountText.text = _currentAmount.ToString();
                transform.DOScale(transform.localScale * 1.1f, 0.1f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.OutBack);
            }
            if (_currentAmount == 0)
            {
                GameEventManager.InvokeTargetReached();
                transform.DOScale(Vector3.zero,0.2f).SetEase(Ease.OutBack).OnComplete(()=> Destroy(gameObject));                
            }
        }

        public void SetTargetUI(Sprite sprite, int requiredAmount, string targetID)
        {
            targetImage.sprite = sprite;
            requiredAmount *= 3;
            requiredAmountText.text = requiredAmount.ToString();
            _targetID = targetID;
            _currentAmount = requiredAmount;
        }
    }
}