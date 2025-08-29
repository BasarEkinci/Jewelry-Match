using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "JewelerySO", menuName = "ScriptableObjects/Jewelery", order = 0)]
    public class JewelryDataSO : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string jewelryID;
        
        public string JewelryID => jewelryID;
        public Sprite Icon => icon;        
    }
}