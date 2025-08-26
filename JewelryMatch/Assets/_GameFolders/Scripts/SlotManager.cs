using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class SlotManager : MonoBehaviour
    {
        [HideInInspector] public List<Jewelry> collectedJewelry = new();
        public List<Transform> slotsTransforms;
        
        public void AddJewelry(Jewelry jewelry)
        {
            if (collectedJewelry.Count < slotsTransforms.Count)
            {
                bool matched = InsertIfExist(jewelry);
                if (!matched)
                    UpdateJewelryPositions();
            }
            else
            {
                Debug.Log("No available slots!");
            }
        }

        private void UpdateJewelryPositions()
        {
            for (int i = 0; i < collectedJewelry.Count; i++)
            {
                collectedJewelry[i].transform.DOMove(slotsTransforms[i].position, 0.2f);
                collectedJewelry[i].transform.DORotate(slotsTransforms[i].eulerAngles, 0.2f);
            }
        }
        private bool InsertIfExist(Jewelry collected)
        {
            int index = collectedJewelry.FindLastIndex(j => j.Type == collected.Type);
            if (index >= 0)
                collectedJewelry.Insert(index + 1, collected);
            else
                collectedJewelry.Add(collected);

            var matchedJewels = collectedJewelry
                .Where(j => j.Type == collected.Type)
                .ToList();

            if (matchedJewels.Count == 3)
            {
                MatchJewelry(matchedJewels).Forget();
                return true;
            }

            return false;
        }
        private async UniTask MatchJewelry(List<Jewelry> matchedObjects)
        {
            Vector3 targetPosition = matchedObjects[1].transform.position;

            var seq = DOTween.Sequence();
            seq.Join(matchedObjects[0].transform.DOMove(targetPosition, 0.2f));
            seq.Join(matchedObjects[2].transform.DOMove(targetPosition, 0.2f));
            seq.OnComplete(() =>
            {
                foreach (var jewel in matchedObjects)
                {
                    collectedJewelry.Remove(jewel);
                    Destroy(jewel.gameObject);
                }
            });
            await seq.AsyncWaitForCompletion();
            UpdateJewelryPositions();
        }
    }
}