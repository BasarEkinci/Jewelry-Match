using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GameFolders.Scripts.UI
{
    public class NavbarButton : MonoBehaviour
    {
        [Header("Preferences")] 
        [SerializeField] private TMP_Text headerText;
        
        [Header("Transform References")]
        [SerializeField] private RectTransform shopPanel;
        [SerializeField] private RectTransform collectionPanel;
        [SerializeField] private RectTransform screenParent;
        [SerializeField] private RectTransform selectedImage;
        
        [Header("Animation Settings")]
        [SerializeField] private float moveSpeed = 0.1f;
        [SerializeField] private Ease moveEase = Ease.Linear;
        [Space]
        [SerializeField] private float scaleAnimDuration = 0.1f;
        [SerializeField] private Ease scaleAnimEase = Ease.OutBack;
        
        private float _screenWidth;
        private float _screenHeight;
        private float _selectedImageOffset;
        private void Start()
        {
            _screenWidth = screenParent.parent.GetComponent<RectTransform>().rect.width;
            _selectedImageOffset = _screenWidth / 3f;
            shopPanel.anchoredPosition = new Vector2(-_screenWidth,0f);
            collectionPanel.anchoredPosition = new Vector2(_screenWidth,0f);
        }

        public void SetScreenPos(int screenWidthOffsetMultiplier)
        {
            Vector2 targetPosition = new Vector2(_screenWidth * screenWidthOffsetMultiplier,0f);
            screenParent.DOAnchorPos(targetPosition,moveSpeed).SetEase(moveEase);
        }

        public void SetSelectedImagePos(int multiplier)
        {
            if (selectedImage == null)
                return;
            selectedImage.DOAnchorPosX(_selectedImageOffset * multiplier,moveSpeed).SetEase(moveEase).OnComplete(()=>
                selectedImage.DOScale(selectedImage.localScale * 1.1f,scaleAnimDuration).SetEase(scaleAnimEase).SetLoops(2,LoopType.Yoyo));
            
            switch (multiplier)
            {
                case 0:
                    headerText.SetText("Home");
                    break;
                case 1:
                    headerText.SetText("Collection");
                    break;
                case -1:
                    headerText.SetText("Shop");
                    break;
            }
        }
    }
}
