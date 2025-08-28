using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            GameStateManager.Instance.ChangeState(Enums.GameState.GamePlaying);
        }
    }
}