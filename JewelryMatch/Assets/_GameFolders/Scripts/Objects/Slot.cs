using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts.Objects
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Ease easeType = Ease.Linear;
        [SerializeField] private float duration = 0.5f;
        public void Animate()
        {
            transform.DOMoveZ(transform.position.z - 0.1f, duration).SetEase(easeType).SetLoops(2, LoopType.Yoyo);
        }
    }
}