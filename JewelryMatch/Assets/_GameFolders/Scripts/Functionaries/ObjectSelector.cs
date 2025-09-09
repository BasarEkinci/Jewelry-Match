using _GameFolders.Scripts.Abstracts;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using _GameFolders.Scripts.Objects;
using UnityEngine;

namespace _GameFolders.Scripts.Functionaries
{
    public class ObjectSelector : MonoBehaviour
    {
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private LayerMask layerMask;

        private CollectableObject _currentCollectable;
        private Camera _camera;
        private GameState _currentState;
        private bool _isPaused;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += state => _currentState = state;
            GameEventManager.OnGamePaused += isPaused => _isPaused = isPaused;
        }

        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= state => _currentState = state;
            GameEventManager.OnGamePaused -= isPaused => _isPaused = isPaused;
        }

        private void Update()
        {
            if (_currentState != GameState.GameStart || _isPaused)
                return;
            
            if (Input.GetMouseButton(0)) 
                HandleHold();
            
            if (_currentCollectable != null)
            {
                _currentCollectable.Select();
                
                if (Input.GetMouseButtonUp(0)) 
                    HandleRelease();
            }
        }

        private void HandleHold()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.TryGetComponent(out CollectableObject collectable))
                {
                    if (_currentCollectable == null)
                    {
                        _currentCollectable = collectable;
                    }
                    else if (_currentCollectable != collectable)
                    {
                        DropCurrent();
                        _currentCollectable = collectable;
                    }
                    AudioManager.Instance.PlaySfx(AudioManager.Instance.AudioData.CollectItemSfx);
                }
                else
                {
                    DropCurrent();
                }
            }
            else
            {
                DropCurrent();
            }
        }

        private void HandleRelease()
        {
            if (_currentCollectable == null) return;

            if (_currentCollectable is Jewelry jewelry)
            {
                _currentCollectable.Collect();
                slotManager.AddJewelry(jewelry);
                GameEventManager.InvokeCollectJewelry(jewelry.JewelryData.JewelryID);
            }
            else
            {
                _currentCollectable.Collect();
            }
            AudioManager.Instance.PlaySfx(AudioManager.Instance.AudioData.CollectItemSfx);
            VibrationHelper.Vibrate(100);
            _currentCollectable = null;
        }

        private void DropCurrent()
        {
            if (_currentCollectable == null) return;

            _currentCollectable.Drop();
            _currentCollectable = null;
        }
    }
}