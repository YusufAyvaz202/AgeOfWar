using Fighter;
using Interfaces;
using Misc;
using ScriptableObjects;
using UI;
using UnityEngine;
using UnityEngine.AI;

namespace Abstracts
{
    public abstract class BaseFighter : MonoBehaviour, IAttacker, IAttackable
    {
        [Header("References")] 
        [SerializeField] private FighterDataSO _fighterDataSo;
        private FighterAnimationController _fighterAnimationController;
        private HealthUI _healthUI;

        [Header("Settings")]
        private FighterType _fighterType;
        private float _attackRange;
        private float _moveSpeed;
        private float _health;
        private float _damage;

        [Header("AI Settings")]
        public Transform _targetDestination;
        private FighterState _fighterState;
        private NavMeshAgent _navMeshAgent;
        [SerializeField] private LayerMask _layerMask;

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            _fighterAnimationController.PlayMoveAnimation();
            FindNearestTarget();
        }

        private void FixedUpdate()
        {
            Move();
        }

        #endregion

        #region AI Methods

        public void Attack()
        {
            if (_fighterState == FighterState.Attacking)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _attackRange, _layerMask);
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out IAttackable attackable))
                    {
                        attackable.TakeDamage(_damage);
                    }
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            _healthUI.UpdateHealthBar(_health);
        }

        private void Move()
        {
            if (_targetDestination == null || _fighterState != FighterState.Move) return;
            FindNearestTarget();
            _navMeshAgent.SetDestination(_targetDestination.position);

            if (Vector2.Distance(transform.position, _targetDestination.position) <= _navMeshAgent.stoppingDistance)
            {
                _fighterAnimationController.PlayAttackAnimation();
                ChangeFighterState(FighterState.Attacking);
            }
        }

        private void FindNearestTarget()
        {
            if (_fighterState == FighterState.Move)
            {
                // TODO: Get nearest fighter from Enemy Spawn Manager.
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.1f, 1f), 0f ,transform.right, _attackRange, _layerMask);
                if (hit.collider != null)
                {
                    _targetDestination = hit.collider.transform;
                }
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

        private void Initialize()
        {
            _fighterType = _fighterDataSo.FighterType;
            _attackRange = _fighterDataSo.AttackRange;
            _moveSpeed = _fighterDataSo.MoveSpeed;
            _health = _fighterDataSo.Health;
            _damage = _fighterDataSo.Damage;

            _fighterAnimationController = GetComponentInChildren<FighterAnimationController>();
            _healthUI = GetComponentInChildren<HealthUI>();
            _healthUI.UpdateMaxHealth(_health);
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
            _navMeshAgent.speed = _moveSpeed;
            
            ChangeFighterState(FighterState.Move);
        }
        
        private void OnDrawGizmos()
        {
            Vector3 origin = transform.position;
            Vector3 direction = transform.right;
            float distance = _attackRange;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, origin + direction * distance);

            if (hit.collider != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(hit.point, 0.05f); 
            }
        }

        #endregion
    }
}