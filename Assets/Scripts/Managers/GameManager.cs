using Misc;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static GameManager Instance;
        
        [Header("Game Settings")]
        private GameState _currentGameState = GameState.Waiting;

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

        [ContextMenu("Dene Game State")]
        public void dene()
        {
            SetGameState(GameState.Playing);
        }
        
        [ContextMenu("Dene Game State 2")]
        public void dene2()
        {
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