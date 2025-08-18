using Interfaces;
using UnityEngine;

namespace Castle
{
    public class Castle : MonoBehaviour, IAttackable
    {
        public float _health = 100f;
        
        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}