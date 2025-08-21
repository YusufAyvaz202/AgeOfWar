using System.Collections.Generic;
using Misc;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static LevelManager Instance;
        
        [Header("Game Data")]
        [SerializeField] private List<LevelDataSO> levelsData = new();
        private int _currentLevelIndex;
        
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


        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }
        
        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion
        
        private void IncreaseLevelIndex()
        {
            _currentLevelIndex++;
            if (_currentLevelIndex >= levelsData.Count)
            {
                _currentLevelIndex = 0;
            }
        }

        public LevelDataSO GetLevelData()
        {
            return levelsData[_currentLevelIndex];
        }

        #region Helper Methods

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Win)
            {
                IncreaseLevelIndex();
            }
        }
        
        public int GetCurrentLevelIndex()
        {
            return _currentLevelIndex;
        }

        public void SetCurrentLevel(int currentLevelIndex)
        {
            _currentLevelIndex = currentLevelIndex;
        }

        #endregion
        
        
    }
}