using _GameFolders.Scripts.Abstracts;
using _GameFolders.Scripts.Enums;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class Jewelry : CollectableObject
    {
        [SerializeField] private float selectedYPosition = 1f;
        [SerializeField] private JewelryType type;
        [SerializeField] private Ease idleEase = Ease.InOutSine;
        public JewelryType Type => type;
        
        private Rigidbody _rigidbody;
        private Collider _collider;
        private Tween _hoverTween;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void OnDestroy()
        {
            _hoverTween?.Kill();
        }

        private void OnDisable()
        {
            _hoverTween?.Kill();
        }

        public override void Collect()
        {
            transform.DOScale(transform.localScale / 2f, 0.1f);
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }

        public void OnMatch()
        {
            Sequence seq = DOTween.Sequence();
            seq.Join(transform.DOMoveZ(transform.position.z + 0.5f, 0.1f));
            seq.Append(transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
        }
        
        public override void Drop()
        {
            _rigidbody.useGravity = true;
        }

        public override void Select()
        {
            _rigidbody.useGravity = false;
            transform.DOLocalMoveY(selectedYPosition, 0.2f);
        }
    }
}
