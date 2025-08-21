using Misc;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static GameManager Instance;

        [Header("Game Settings")] 
        private GameState _currentGameState;

        #region Unity Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SetGameState(GameState.Paused);
            SetGameState(GameState.Waiting);
        }

        #endregion
        
        public void SetGameState(GameState gameState)
        {
            if (_currentGameState == gameState) return;
            _currentGameState = gameState;
            EventManager.OnGameStateChanged?.Invoke(_currentGameState);
            Debug.Log($"Game State Changed: {_currentGameState}");
        }
    }
}