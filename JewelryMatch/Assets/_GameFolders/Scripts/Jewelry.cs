using System;
using System.Runtime.CompilerServices;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class Jewelry : MonoBehaviour, ICollectable
    {
        [SerializeField] private float selectedYPosition = 1f;
        [SerializeField] private JewelryType type;
        public JewelryType Type => type;
        
        private Rigidbody _rigidbody;
        private Tween _hoverTween;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Collect()
        {
            Debug.Log("Collected: " + type);
            transform.DOScale(Vector3.zero, 0.1f);
        }

        public void Drop()
        {
            _rigidbody.useGravity = true;
            Debug.Log("Dropped: " + type);
        }

        public void Select()
        {
            Debug.Log("Selected: " + type);
            _rigidbody.useGravity = false;
            transform.DOLocalMoveY(selectedYPosition, 0.2f);
        }
    }
}
