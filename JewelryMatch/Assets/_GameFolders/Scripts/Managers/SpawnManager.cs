using System.Collections.Generic;
using _GameFolders.Scripts.Data.UnityObjects;
using _GameFolders.Scripts.Data.ValueObjects;
using _GameFolders.Scripts.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private Vector3 lowerSpawnBounds;
        [SerializeField] private Vector3 upperSpawnBounds;
        
        private void OnEnable()
        {
            GameEventManager.OnGameStateChanged += HandleGameStateChange;
        }
        
        private void OnDisable()
        {
            GameEventManager.OnGameStateChanged -= HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState state)
        {
            if (state == GameState.GameStart)
            {
                SpawnJewelry().Forget();
            }
        }

        private async UniTaskVoid SpawnJewelry()
        {
            for (int i = 0; i < InitializeSpawnList().Count; i++)
            {
                Instantiate(InitializeSpawnList()[i].targetJewelry, GenerateRandomPosition(), Quaternion.identity);
                await UniTask.Delay(250);
            }            
        }

        private List<JewelryToSpawnData> InitializeSpawnList()
        {
            List<JewelryToSpawnData> allJewelriesToSpawn = new List<JewelryToSpawnData>();
            LevelDataSO currentLevel = GameManager.Instance.CurrentLevel;
            
            foreach (var target in currentLevel.TargetData)
            {
                int count = target.requiredMatchCount * 3;
                for (int i = 0; i < count; i++)
                {
                    allJewelriesToSpawn.Add(target);
                }
            }
            foreach (var other in currentLevel.OtherJewelriesToSpawn)
            {
                int count = other.requiredMatchCount * 3;
                for (int i = 0; i < count; i++)
                {
                    allJewelriesToSpawn.Add(other);
                }
            }
            return allJewelriesToSpawn;
        }
        
        private Vector3 GenerateRandomPosition()
        {
            float xPos = Random.Range(lowerSpawnBounds.x, upperSpawnBounds.x);
            float yPos = upperSpawnBounds.y;
            float zPos = Random.Range(lowerSpawnBounds.z, upperSpawnBounds.z);
            return new Vector3(xPos, yPos, zPos);
        }
    }
}