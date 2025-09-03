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

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += state => _currentState = state;
        }
        
        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= state => _currentState = state;
        }

        private void Update()
        {
            if (_currentState != GameState.GameStart) return;
            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))
                {
                    if (hit.collider.TryGetComponent<CollectableObject>(out CollectableObject collectable))
                    {
                        _currentCollectable = collectable;
                    }
                    else
                    {
                        if (_currentCollectable != null)
                        {
                            _currentCollectable.Drop(); 
                            _currentCollectable = null;
                        }
                    }
                }
            } 
            if (_currentCollectable != null)
            { 
                _currentCollectable.Select();
                if (Input.GetMouseButtonUp(0))
                {
                    if (_currentCollectable is Jewelry)
                    {
                        Jewelry jewelry = _currentCollectable as Jewelry;
                        _currentCollectable.Collect();
                        slotManager.AddJewelry(jewelry);
                        GameEventManager.InvokeCollectJewelry(jewelry.JewelryData.JewelryID);
                    }
                    else
                    {
                        _currentCollectable.Collect();
                    }
                    _currentCollectable = null; 
                } 
            }
        }
    }
}