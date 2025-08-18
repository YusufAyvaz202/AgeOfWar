using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Image _healthBarImage;
        private float _maxHealth;
        
        public void UpdateHealthBar(float health)
        {
            _healthBarImage.fillAmount = health / _maxHealth;
        }

        public void UpdateMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
        }
    }
}