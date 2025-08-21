using Interfaces;
using Managers;
using Misc;
using UI;
using UnityEngine;

namespace Castle
{
    public class Castle : MonoBehaviour, IAttackable
    {
        [Header("Settings")]
        [SerializeField] private float _baseHealth;
        private float _currentHealth;
        [SerializeField] private bool _isPlayerCastle;
        private HealthUI _healthUI;
        private bool _isPlaying;

        #region Unity Methods

        private void Awake()
        {
            _currentHealth = _baseHealth;
            _healthUI = GetComponentInChildren<HealthUI>();
            _healthUI.UpdateMaxHealth(_currentHealth);
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
        
        public void TakeDamage(float damage)
        {
            if (!_isPlaying) return;
            
            _currentHealth -= damage;
            _healthUI.UpdateHealthBar(_currentHealth);

            if (_currentHealth <= 0)
            {
                GameManager.Instance.SetGameState(_isPlayerCastle ? GameState.Lose : GameState.Win);
            }
        }

        #region Helper Methods

        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
            if (gameState == GameState.Playing)
            {
                _isPlaying = true;
            }
            else if (gameState == GameState.Win || gameState == GameState.Lose)
            {
                _isPlaying = false;
                _currentHealth = _baseHealth; 
                _healthUI.UpdateHealthBar(_baseHealth);
            }
            else
            {
                _isPlaying = false;
            }
        }

        #endregion
    }
}