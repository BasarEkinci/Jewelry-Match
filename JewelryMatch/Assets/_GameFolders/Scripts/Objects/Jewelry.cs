using System.Collections.Generic;
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

        private Tween _hoverTween;
        private Rigidbody _rb;
        private List<Collider> _coll; //Some jewelry has multiple colliders
        private MeshRenderer _mesh;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _coll = new List<Collider>(GetComponents<Collider>());
            _mesh = GetComponentInChildren<MeshRenderer>();
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
            _coll.ForEach(c=> c.enabled = false);
            _mesh.shadowCastingMode = ShadowCastingMode.Off;
        }

        public void OnMatch()
        {
            Sequence seq = DOTween.Sequence();
            seq.Join(transform.DOMoveZ(transform.position.z + 0.5f, 0.1f));
            seq.Append(transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
        }
        
        public override void Drop()
        {
            _rb.useGravity = true;
            _rb.isKinematic = false;
        }

        public override void Select()
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            transform.DOLocalMoveY(selectedYPosition, 0.2f);
        }
    }
}
