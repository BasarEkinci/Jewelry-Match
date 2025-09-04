using _GameFolders.Scripts.Abstracts;
using _GameFolders.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts.Objects
{
    public class Hourglass : CollectableObject
    {
        [SerializeField] private float additionalTime = 15f;
        [SerializeField] private float riseAmount = 2f;
        [SerializeField] private float riseDuration = 0.5f;
        [SerializeField] private float rotateDuration = 0.5f;
        [SerializeField] private float scaleDuration = 0.5f;
        [SerializeField] private float selectedYPosition = 1f;
        
        private static readonly int OutlineMultiplier = Shader.PropertyToID("_OutlineMultiplier");
        
        private Sequence _sequence;
        private Rigidbody _rigidbody;
        private Material _outlineMaterial;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _outlineMaterial = GetComponent<MeshRenderer>().materials[1];
        }

        private void OnEnable()
        {
            _sequence = DOTween.Sequence();
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }

        public override void Collect()
        {
            GameEventManager.InvokeHourglassCollected(additionalTime);
            _sequence.Join(transform.DOMoveY(transform.position.y + riseAmount, riseDuration));
            _sequence.Join(transform.DORotate(Vector3.one * 45f, rotateDuration).SetLoops(5, LoopType.Incremental).SetEase(Ease.Linear));
            _sequence.Join(transform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.InBack).OnComplete(()=> Destroy(gameObject)));
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        public override void Drop()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _outlineMaterial.SetFloat(OutlineMultiplier, 0f);
        }

        public override void Select()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _outlineMaterial.SetFloat(OutlineMultiplier, 1f);
            transform.DOLocalMoveY(selectedYPosition, 0.2f);
        }
    }
}