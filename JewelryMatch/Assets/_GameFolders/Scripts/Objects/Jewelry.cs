using _GameFolders.Scripts.Abstracts;
using _GameFolders.Scripts.Data.UnityObjects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

namespace _GameFolders.Scripts.Objects
{
    public class Jewelry : CollectableObject
    {
        [Header("Components")]
        [SerializeField] private JewelryDataSO jewelryData;
        
        [Header("Settings")]
        [SerializeField] private float selectedYPosition = 1f;
        [SerializeField] private float collectedScale = 0.6f;
        [SerializeField] private float scaleDurationAfterCollect = 0.1f;
        
        public JewelryDataSO JewelryData => jewelryData;
        
        private static readonly int OutlineMultiplier = Shader.PropertyToID("_OutlineMultiplier");

        private Tween _hoverTween;
        private Rigidbody _rb;
        private Collider _coll; 
        private MeshRenderer _mesh;
        private Material _outlineMaterial;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _coll = GetComponent<Collider>();
            _mesh = GetComponentInChildren<MeshRenderer>();
            _outlineMaterial = _mesh.materials[1];
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
            transform.DOScale(collectedScale, scaleDurationAfterCollect);
            _rb.isKinematic = true;
            _coll.enabled = false;
            _mesh.shadowCastingMode = ShadowCastingMode.Off;
            _outlineMaterial.SetFloat(OutlineMultiplier, 0f);
        }

        public void OnMatch()
        {
            Sequence seq = DOTween.Sequence();
            seq.Join(transform.DOMoveZ(transform.position.z + 0.5f, 0.1f));
            seq.Append(transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
        }
        
        public override void Drop()
        {
            _rb.isKinematic = false;
            _rb.useGravity = true;
            _outlineMaterial.SetFloat(OutlineMultiplier, 0f);
        }

        public override void Select()
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            _outlineMaterial.SetFloat(OutlineMultiplier, 1f);
            transform.DOLocalMoveY(selectedYPosition, 0.2f);
        }
    }
}
