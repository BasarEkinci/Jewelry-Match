using System;
using System.Collections.Generic;
using System.Linq;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Functionaries;
using _GameFolders.Scripts.Objects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> slotsTransforms;
        [SerializeField] private List<Slot> slots;
        
        private readonly List<Jewelry> _collectedJewelry = new();
        private const float RepositionDuration = 0.2f;

        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += HandleGameStateChange;
        }

        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandleGameStateChange;
        }

        public void AddJewelry(Jewelry jewelry)
        {
            InsertIfExist(jewelry).Forget();
            bool hasSpace = _collectedJewelry.Count < slotsTransforms.Count;
            bool completesTriplet = WillMatchWithIncoming(jewelry);
            if (hasSpace || completesTriplet)
            {
                return;
            }
            GameEventManager.InvokeGameStateChanged(GameState.NoMoreSpaces);
        }

        private bool WillMatchWithIncoming(Jewelry incoming)
        {
            int same = _collectedJewelry.Count(j => j.JewelryData.JewelryID == incoming.JewelryData.JewelryID);
            return same == 3;
        }

        private UniTask UpdateJewelryPositionsAsync()
        {
            var tasks = new List<UniTask>(_collectedJewelry.Count * 2);
            for (int i = 0; i < _collectedJewelry.Count; i++)
            {
                var jew = _collectedJewelry[i];
                var t = jew.transform;

                t.DOKill();
                t.SetParent(slotsTransforms[i], true);

                Vector3 targetPos = slotsTransforms[i].position + jew.CollectedPositionOffset;
                Vector3 targetRot = slotsTransforms[i].eulerAngles + jew.RotationOffset;

                tasks.Add(t.DOMove(targetPos, RepositionDuration)
                    .SetEase(Ease.OutQuad)
                    .AsyncWaitForCompletion()
                    .AsUniTask());
                tasks.Add(t.DORotate(targetRot, RepositionDuration)
                    .SetEase(Ease.OutQuad)
                    .AsyncWaitForCompletion()
                    .AsUniTask());
            }
            return UniTask.WhenAll(tasks);
        }
        private async UniTaskVoid InsertIfExist(Jewelry collected)
        {
            int index = _collectedJewelry.FindLastIndex(j => j.JewelryData.JewelryID == collected.JewelryData.JewelryID);
            if (index >= 0)
                _collectedJewelry.Insert(index + 1, collected);
            else
                _collectedJewelry.Add(collected);

            await UpdateJewelryPositionsAsync();
            
            int slotIdx = _collectedJewelry.IndexOf(collected);
            if (slotIdx >= 0 && slotIdx < slots.Count)
                slots[slotIdx].Animate();

            var matchedJewels = _collectedJewelry
                .Where(j => j.JewelryData.JewelryID == collected.JewelryData.JewelryID)
                .ToList();

            if (matchedJewels.Count == 3)
            {
                await MatchJewelryAsync(matchedJewels);
                await UpdateJewelryPositionsAsync();
            }
        }

        private async UniTask MatchJewelryAsync(List<Jewelry> matched)
        {
            try
            {
                int centerIndex = _collectedJewelry.IndexOf(matched[1]);
                if (centerIndex < 0) return;

                float targetX = slotsTransforms[centerIndex].position.x;

                foreach (var j in matched)
                {
                    j.transform.DOKill();
                    j.transform.DOMoveX(targetX, 0.1f);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

                foreach (var jewelry in matched)
                {
                    _collectedJewelry.Remove(jewelry);
                    jewelry.OnMatch();
                }

                AudioManager.Instance.PlaySfx(AudioManager.Instance.AudioData.MatchSoundSfx);
                VibrationHelper.Vibrate(100);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        private void HandleGameStateChange(GameState state)
        {
            if (state is GameState.MainMenu or GameState.GameOver)
            {
                foreach (var jew in _collectedJewelry)
                {
                    jew.transform.DOScale(Vector3.zero, 0.1f);
                }
                for (int i = _collectedJewelry.Count - 1; i >= 0 ; i--)
                {
                    Destroy(_collectedJewelry[i].gameObject);
                    _collectedJewelry.RemoveAt(i);
                }
            }
        }
    }
}