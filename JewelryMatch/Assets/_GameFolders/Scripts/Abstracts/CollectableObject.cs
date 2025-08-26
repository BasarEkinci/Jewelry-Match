using UnityEngine;

namespace _GameFolders.Scripts.Abstracts
{
    public abstract class CollectableObject : MonoBehaviour
    {
        public abstract void Collect();
        public abstract void Drop();
        public abstract void Select();
    }
}