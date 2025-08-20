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
        [SerializeField] private float _health = 100f;
        [SerializeField] private bool _isPlayerCastle;
        private HealthUI _healthUI;
        private bool _isPlaying;

        #region Unity Methods

        private void Awake()
        {
            _healthUI = GetComponentInChildren<HealthUI>();
            _healthUI.UpdateMaxHealth(_health);
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
            
            _health -= damage;
            _healthUI.UpdateHealthBar(_health);

            if (_health <= 0)
            {
                GameManager.Instance.SetGameState(_isPlayerCastle ? GameState.Lose : GameState.Win);
            }
        }

        #region Helper Methods

        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }

        #endregion
    }
}