using _GameFolders.Scripts.Data.UnityObjects;
using _GameFolders.Scripts.Data.ValueObjects;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using _GameFolders.Scripts.Objects;
using UnityEngine;

namespace _GameFolders.Scripts.Functionaries
{
    public class TargetContainer : MonoBehaviour
    {
        [SerializeField] private GameObject targetUIContainer;
        [SerializeField] private TargetUI targetUi;
        private GameState _gameState;
        private void OnEnable()
        {
            LevelDataSO currentLevel = GameManager.Instance.CurrentLevel; 
            for (int i = 0; i < currentLevel.TargetData.Count; i++) 
            { 
                TargetUI targetUI = Instantiate(targetUi, targetUIContainer.transform); 
                JewelryToSpawnData jewelryToSpawn = currentLevel.TargetData[i]; 
                targetUI.SetTargetUI(jewelryToSpawn.targetJewelry.JewelryData.Icon, 
                    jewelryToSpawn.requiredMatchCount, jewelryToSpawn.targetJewelry.JewelryData.JewelryID);
            }   
        }

        private void OnDisable()
        {
            foreach (Transform child in targetUIContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
        private void FixedUpdate()
        {
            if (transform.childCount == 0)
            {
                GameEventManager.InvokeGameStateChanged(GameState.GameWin);
                Debug.Log("Level Completed!");
            }
        }
    }
}