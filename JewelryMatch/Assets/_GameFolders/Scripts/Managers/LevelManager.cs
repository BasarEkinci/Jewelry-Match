using System.Collections.Generic;
using _GameFolders.Scripts.Data;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<LevelDataSO> levelData;
        
        public LevelDataSO LevelData => levelData[0];
        public int CurrentLevelIndex => _currentLevelIndex;
        
        private int _currentLevelIndex = 0;

        private void Start()
        {
            _currentLevelIndex = 0;
        }

        private void LoadLevel()
        {
            
        }
    }
}