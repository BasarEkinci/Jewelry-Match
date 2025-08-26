using UnityEngine;

namespace _GameFolders.Scripts
{
    public class ObjectSelector : MonoBehaviour
    {
        [SerializeField] private SlotManager slotManager;
        private Jewelry _currentJewelry; 
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
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
                    _currentJewelry = null; 
                } 
            }
        }
    }
}