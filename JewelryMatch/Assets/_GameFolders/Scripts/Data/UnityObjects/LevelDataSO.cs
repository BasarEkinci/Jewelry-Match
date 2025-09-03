using System.Collections.Generic;
using System.Linq;
using _GameFolders.Scripts.Abstracts;
using _GameFolders.Scripts.Data.ValueObjects;
using UnityEditor;
using UnityEngine;

namespace _GameFolders.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "LevelDataSO", menuName = "ScriptableObjects/LevelData", order = 0)]
    public class LevelDataSO : ScriptableObject
    {
        [Header("Level Settings")]
        [SerializeField] private float levelTime = 60f;

        [Header("PowerUp List")]
        [SerializeField] private List<PowerUpToSpawnData> powerUps;
        
        [Header("Jewelry Data Lists")]
        [SerializeField] private List<JewelryToSpawnData> otherJewelriesToSpawn;
        [SerializeField] private List<JewelryToSpawnData> targetData;
        
        /*
         * Powerups will be added
         */
        public List<PowerUpToSpawnData> PowerUps => powerUps;
        public float LevelTime => levelTime;
        public List<JewelryToSpawnData> TargetData => targetData;
        public List<JewelryToSpawnData> OtherJewelriesToSpawn => otherJewelriesToSpawn;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            var targetJewels = targetData.Where(t => t.targetJewelry != null)
                .Select(t => t.targetJewelry)
                .ToList();
            
            var duplicates = otherJewelriesToSpawn
                .Where(o => o.targetJewelry != null && targetJewels.Contains(o.targetJewelry))
                .ToList();

            if (duplicates.Count > 0)
            {
                string dupNames = string.Join(", ", duplicates.Select(d => d.targetJewelry.name));
                
                otherJewelriesToSpawn = otherJewelriesToSpawn
                    .Where(o => o.targetJewelry != null && !targetJewels.Contains(o.targetJewelry))
                    .ToList();

                Debug.LogWarning($"[LevelDataSO] Duplicate object detected at OtherJewelriesToSpawn list: {dupNames}", this);
                
                EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}