using Interfaces;
using Misc;
using ScriptableObjects;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseFighter : MonoBehaviour, IAttacker, IAttackable
    {
        [Header("References")] 
        [SerializeField] private FighterDataSO _fighterDataSo;

        [Header("Settings")]
        private FighterType _fighterType;
        private float _attackSpeed;
        private float _moveSpeed;
        private float _health;
        private float _damage;

        #region Unity Methods

        private void Awake()
        {
            _fighterType = _fighterDataSo._fighterType;
            _attackSpeed = _fighterDataSo.AttackSpeed;
            _moveSpeed = _fighterDataSo.MoveSpeed;
            _health = _fighterDataSo.Health;
            _damage = _fighterDataSo.Damage;
        }

        #endregion

        public abstract void Attack();

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}