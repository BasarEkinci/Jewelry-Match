using System;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GameState gameState;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(HandleButtonAction);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleButtonAction);
        }

        private void HandleButtonAction()
        {
            if (HealthManager.Instance.HealthAmount == 0 && gameState == GameState.GameStart)
                return;
            GameEventManager.InvokeGameStateChanged(gameState);
        }
    }
}