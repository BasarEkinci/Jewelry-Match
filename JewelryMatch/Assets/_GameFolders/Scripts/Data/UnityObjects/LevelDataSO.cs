using System.Collections.Generic;
using _GameFolders.Scripts.Data.ValueObjects;
using UnityEngine;

namespace _GameFolders.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "LevelDataSO", menuName = "ScriptableObjects/LevelData", order = 0)]
    public class LevelDataSO : ScriptableObject
    {
        [Header("Level Settings")]
        [SerializeField] private float levelTime = 60f;
        
        [Header("Jewelry Data Lists")]
        [SerializeField] private List<Jewelry> jewelriesToSpawn;
        [SerializeField] private List<TargetData> targetData;
        
        /*
         * Powerups will be added
         */
        
        public List<Jewelry> JewelriesToSpawn => jewelriesToSpawn;
        public List<TargetData> TargetData => targetData;
    }
}