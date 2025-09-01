using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using UnityEngine;

namespace _GameFolders.Scripts.UI
{
    public class ButtonController : MonoBehaviour
    {
        public void PlayButton()
        {
            if (HealthManager.Instance.HealthAmount == 0)
                return;
            GameEventManager.InvokeGameStateChanged(GameState.GameStart);
        }
        public void ContinueButton()
        {
            GameEventManager.InvokeGameStateChanged(GameState.MainMenu);            
        }
        public void CloseLosePanel()
        {
            GameEventManager.InvokeGameStateChanged(GameState.GameOver);// This is for returning to main menu after losing
        }
    }
}