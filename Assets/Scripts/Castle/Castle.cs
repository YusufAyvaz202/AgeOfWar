using Interfaces;
using UI;
using UnityEngine;

namespace Castle
{
    public class Castle : MonoBehaviour, IAttackable
    {
        [Header("Settings")]
        [SerializeField] private float _health = 100f;
        private HealthUI _healthUI;

        #region Unity Methods

        private void Awake()
        {
            _healthUI = GetComponentInChildren<HealthUI>();
            _healthUI.UpdateMaxHealth(_health);
        }

        #endregion
        
        public void TakeDamage(float damage)
        {
            _health -= damage;
            _healthUI.UpdateHealthBar(_health);

            if (_health <= 0)
            {
                // TODO: Determine win or lose with GameStates
            }
        }
        
        
    }
}