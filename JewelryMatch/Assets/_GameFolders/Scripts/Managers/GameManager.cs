using TMPro;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        
        [SerializeField] private TMP_Text requiredJewelText;
        [SerializeField] private TMP_Text requiredJewelCountText;
        
        
        private int _requiredJewelCount;
        private int _currentJewelCount;
        private string _requiredJewelID;

        private void OnEnable()
        {
            if (levelManager != null)
            {
                _requiredJewelCount = levelManager.LevelData.TargetData[0].requiredAmount;
                _requiredJewelID = levelManager.LevelData.TargetData[0].targetJewelry.JewelryData.JewelryID;
            }
            
            requiredJewelCountText.text = _requiredJewelCount.ToString();
            requiredJewelText.text = _requiredJewelID;
        }
    }
}