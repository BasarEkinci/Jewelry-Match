using System;
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
        [SerializeField] private List<Transform> slotsTransforms;
        [SerializeField] private List<Slot> slots;
        
        public void AddJewelry(Jewelry jewelry)
        {
            if (collectedJewelry.Count < slotsTransforms.Count)
            {
                InsertIfExist(jewelry).Forget();
            }
            else
            {
                Debug.Log("No available slots!");
            }
        }

        private float UpdateJewelryPositions()
        {
            float duration = 0.2f; 
            for (int i = 0; i < collectedJewelry.Count; i++)
            {
                collectedJewelry[i].transform.DOMove(slotsTransforms[i].position, duration);
                collectedJewelry[i].transform.DORotate(slotsTransforms[i].eulerAngles, duration);
            }
            return duration;
        }

        private async UniTaskVoid InsertIfExist(Jewelry collected)
        {
            int index = collectedJewelry.FindLastIndex(j => j.Type == collected.Type);
            if (index >= 0)
                collectedJewelry.Insert(index + 1, collected);
            else
                collectedJewelry.Add(collected);

            float duration = UpdateJewelryPositions();
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            slots[collectedJewelry.IndexOf(collected)].Animate();

            var matchedJewels = collectedJewelry
                .Where(j => j.Type == collected.Type)
                .ToList();

            if (matchedJewels.Count == 3)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(duration));
                MatchJewelry(matchedJewels);
                await UniTask.Delay(TimeSpan.FromSeconds(duration));
                UpdateJewelryPositions();
            }
        }

        private async void MatchJewelry(List<Jewelry> matched)
        {
            float targetXPosition = slotsTransforms[collectedJewelry.IndexOf(matched[1])].position.x;
            Debug.Log(targetXPosition);
            matched[0].transform.DOMoveX(targetXPosition, 0.1f);
            matched[2].transform.DOMoveX(targetXPosition, 0.1f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            foreach (var jewelry in matched)
            {
                collectedJewelry.Remove(jewelry);
                jewelry.OnMatch();
            }
        }
    }
}