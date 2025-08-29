using System;
using System.Collections.Generic;
using System.Linq;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using _GameFolders.Scripts.Objects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> slotsTransforms;
        [SerializeField] private List<Slot> slots;
        
        private readonly List<Jewelry> _collectedJewelry = new();
        public void AddJewelry(Jewelry jewelry)
        {
            InsertIfExist(jewelry).Forget();
            bool hasSpace = _collectedJewelry.Count < slotsTransforms.Count;
            bool completesTriplet = WillMatchWithIncoming(jewelry);
            if (hasSpace || completesTriplet)
            {
                return;
            }
            GameManager.Instance.OnGameStateChanged.Invoke(GameState.GameOver);
        }

        private bool WillMatchWithIncoming(Jewelry incoming)
        {
            int same = _collectedJewelry.Count(j => j.JewelryData.JewelryID == incoming.JewelryData.JewelryID);
            return same == 3;
        }

        private float UpdateJewelryPositions()
        {
            float duration = 0.2f; 
            for (int i = 0; i < _collectedJewelry.Count; i++)
            {
                _collectedJewelry[i].transform.DOMove(slotsTransforms[i].position, duration);
                _collectedJewelry[i].transform.DORotate(slotsTransforms[i].eulerAngles, duration);
            }
            return duration;
        }

        private async UniTaskVoid InsertIfExist(Jewelry collected)
        {
            int index = _collectedJewelry.
                FindLastIndex(j => j.JewelryData.JewelryID == collected.JewelryData.JewelryID);
            if (index >= 0)
                _collectedJewelry.Insert(index + 1, collected);
            else
                _collectedJewelry.Add(collected);

            float duration = UpdateJewelryPositions();
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            slots[_collectedJewelry.IndexOf(collected)].Animate();

            var matchedJewels = _collectedJewelry
                .Where(j => j.JewelryData.JewelryID == collected.JewelryData.JewelryID)
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
            try
            {
                float targetXPosition = slotsTransforms[_collectedJewelry.IndexOf(matched[1])].position.x;
                matched[0].transform.DOMoveX(targetXPosition, 0.1f);
                matched[2].transform.DOMoveX(targetXPosition, 0.1f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                foreach (var jewelry in matched)
                {
                    _collectedJewelry.Remove(jewelry);
                    jewelry.OnMatch();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error during matching jewelry", e);
            }
        }
    }
}