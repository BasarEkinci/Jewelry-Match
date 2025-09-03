using System.Collections.Generic;
using _GameFolders.Scripts.Abstracts;
using _GameFolders.Scripts.Data.UnityObjects;
using _GameFolders.Scripts.Enums;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject jewelryHolder;
        
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
                SpawnJewelry();
            }
            else if (state is GameState.GameOver or GameState.MainMenu)
            {
                for (int i = jewelryHolder.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(jewelryHolder.transform.GetChild(i).gameObject);
                }    
            }
        }

        private void SpawnJewelry()
        {
            for (int i = 0; i < InitializeSpawnList().Count; i++)
            {
                Instantiate(InitializeSpawnList()[i], GenerateRandomPosition(), Quaternion.identity, jewelryHolder.transform);
            }            
        }

        private List<CollectableObject> InitializeSpawnList()
        {
            List<CollectableObject> allItemsToSpawn = new List<CollectableObject>();
            LevelDataSO currentLevel = GameManager.Instance.CurrentLevel;
            
            foreach (var target in currentLevel.TargetData)
            {
                CollectableObject collectable = target.targetJewelry.GetComponent<CollectableObject>();
                int count = target.requiredMatchCount * 3;
                for (int i = 0; i < count; i++)
                {
                    allItemsToSpawn.Add(collectable);
                }
            }
            foreach (var other in currentLevel.OtherJewelriesToSpawn)
            {
                CollectableObject collectable = other.targetJewelry.GetComponent<CollectableObject>();
                int count = other.requiredMatchCount * 3;
                for (int i = 0; i < count; i++)
                {
                    allItemsToSpawn.Add(collectable);
                }
            }

            foreach (var powerUp in currentLevel.PowerUps)
            {
                CollectableObject collectable = powerUp.powerUp.GetComponent<CollectableObject>();
                int count = powerUp.spawnCount;
                for (int i = 0; i < count; i++)
                {
                    allItemsToSpawn.Add(collectable);
                }
            }
            return allItemsToSpawn;
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