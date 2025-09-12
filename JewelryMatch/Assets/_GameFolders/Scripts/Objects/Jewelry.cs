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
        [SerializeField] private Vector3 rotationOffset = Vector3.zero;
        [SerializeField] private Vector3 collectedPositionOffset = Vector3.zero;
        
        public Vector3 RotationOffset => rotationOffset;
        public Vector3 CollectedPositionOffset => collectedPositionOffset;
        public JewelryDataSO JewelryData => jewelryData;
        
        private static readonly int OutlineMultiplier = Shader.PropertyToID("_OutlineMultiplier");
        
        private Rigidbody _rb;
        private Collider _coll; 
        private MeshRenderer _mesh;
        private Material _outlineMaterial;
        private MaterialPropertyBlock _mpb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _coll = GetComponent<Collider>();
            _mesh = GetComponentInChildren<MeshRenderer>();
            _mpb = new MaterialPropertyBlock();
        }

        public override void Collect()
        {
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _coll.enabled = false;
            _mesh.shadowCastingMode = ShadowCastingMode.Off;
            SetOutline(0f);
            transform.localScale = Vector3.one * collectedScale;
        }

        public void SetCollectedValues()
        {
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _coll.enabled = false;
            _mesh.shadowCastingMode = ShadowCastingMode.Off;
            SetOutline(0f);
        }
        private void SetOutline(float value)
        {
            _mesh.GetPropertyBlock(_mpb);
            _mpb.SetFloat(OutlineMultiplier, value);
            _mesh.SetPropertyBlock(_mpb); 
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
            SetOutline(0f);
        }

        public override void Select()
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            SetOutline(1f);
            transform.DOLocalMoveY(selectedYPosition, 0.2f);
        }
        
    }
}
