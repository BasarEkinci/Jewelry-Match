using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using _GameFolders.Scripts.Objects;
using Sirenix.OdinValidator.Editor;
using UnityEngine;

namespace _GameFolders.Scripts.Functionaries
{
    public class ObjectSelector : MonoBehaviour
    {
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private LayerMask layerMask;
        private Jewelry _currentJewelry; 
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
                    if (hit.collider.TryGetComponent<Jewelry>(out Jewelry collectable))
                    {
                        _currentJewelry = collectable;
                    }
                    else
                    {
                        if (_currentJewelry != null)
                        {
                            _currentJewelry.Drop(); 
                            _currentJewelry = null;
                        }
                    }
                }
            } 
            if (_currentJewelry != null)
            { 
                _currentJewelry.Select();
                if (Input.GetMouseButtonUp(0))
                { 
                    _currentJewelry.Collect();
                    slotManager.AddJewelry(_currentJewelry);
                    GameEventManager.InvokeCollectJewelry(_currentJewelry.JewelryData.JewelryID);
                    _currentJewelry = null; 
                } 
            }
        }
    }
}