using _GameFolders.Scripts.Interfaces;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class ObjectSelector : MonoBehaviour
    {
        private ICollectable _currentCollectable;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        { 
            if (Input.GetMouseButton(0)) 
            {
                if (TryGetCollectable())
                {
                    if (Input.GetMouseButtonUp(0))
                        _currentCollectable.Collect();
                    else
                        _currentCollectable.Drop();
                    _currentCollectable.Select();
                }
            }
        }

        private bool TryGetCollectable()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider.TryGetComponent<ICollectable>(out ICollectable collectable))
                    _currentCollectable = collectable;
                else
                    _currentCollectable = null;
            }
            return _currentCollectable != null;
        }
    }
}