using Fighter;
using Interfaces;
using Misc;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Abstracts
{
    public abstract class BaseFighter : MonoBehaviour, IAttacker, IAttackable
    {
        [Header("References")] 
        [SerializeField] private FighterDataSO _fighterDataSo;
        private FighterAnimationController _fighterAnimationController;
        private FighterHealthUI _fighterHealthUI;

        [Header("Settings")]
        private FighterType _fighterType;
        private float _attackSpeed;
        private float _moveSpeed;
        private float _health;
        private float _damage;

        [Header("AI Settings")]
        public Transform _targetDestination;
        private FighterState _fighterState;
        private NavMeshAgent _navMeshAgent;

        #region Unity Methods

        private void Awake()
        {
            _fighterType = _fighterDataSo.FighterType;
            _attackSpeed = _fighterDataSo.AttackSpeed;
            _moveSpeed = _fighterDataSo.MoveSpeed;
            _health = _fighterDataSo.Health;
            _damage = _fighterDataSo.Damage;

            _fighterAnimationController = GetComponent<FighterAnimationController>();
            _fighterHealthUI = GetComponentInChildren<FighterHealthUI>();
            _fighterHealthUI.UpdateMaxHealth(_health);
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
            _navMeshAgent.speed = _moveSpeed;
            
            ChangeFighterState(FighterState.Move);
        }

        private void Start()
        {
            _fighterAnimationController.PlayMoveAnimation();
        }

        private void FixedUpdate()
        {
            Move();
        }

        #endregion

        #region AI Methods

        public void Attack()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out IAttackable attackable))
                {
                    attackable.TakeDamage(_damage);
                }
                Debug.Log(hit.collider.gameObject.name);
            }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            _fighterHealthUI.UpdateHealthBar(_health);
        }

        private void Move()
        {
            if (_targetDestination == null || _fighterState != FighterState.Move) return;
            _navMeshAgent.SetDestination(_targetDestination.position);

            if (Vector2.Distance(transform.position, _targetDestination.position) <= _navMeshAgent.stoppingDistance)
            {
                if (_fighterState == FighterState.Attacking) return;
                ChangeFighterState(FighterState.Attacking);  
                _fighterAnimationController.PlayAttackAnimation();
                Attack();
            }
        }

        private void FindNearestTarget()
        {
            if (_fighterState == FighterState.Move)
            {
                // TODO: Get nearest fighter from Enemy Spawn Manager.
            }
        }

        #endregion

        #region Helper Methods

        private void ChangeFighterState(FighterState fighterState)
        {
            _fighterState = fighterState;
        }

        public void SetTargetDestination(Transform target)
        {
            _targetDestination = target;
        }

        #endregion
    }
}