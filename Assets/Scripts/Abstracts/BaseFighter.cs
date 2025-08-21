using Fighter;
using Interfaces;
using Managers;
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
        [SerializeField] private FighterType _fighterType;
        private float _moveSpeed;
        private float _health;
        protected float _attackRange;
        protected float _damage;

        [Header("AI Settings")]
        [SerializeField] protected LayerMask _layerMask;
        protected FighterState _fighterState;
        private NavMeshAgent _navMeshAgent;
        private Transform _targetDestination;

        [Header("Game Settings")]
        [SerializeField] private bool _isPlayerFighter;
        protected bool _isPlaying = true;

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void Start()
        {
            ChangeFighterState(FighterState.Move);
        }

        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            FindNearestTarget();
            Move();
        }

        private void OnDestroy()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion

        #region Base Methods

        /// <summary> This method is called from the animation event in the fighter's attack animation on FighterAnimationEventController.cs. /// </summary>
        public abstract void Attack();

        public void TakeDamage(float damage)
        {
            if (!_isPlaying) return;
            _health -= damage;
            _healthUI.UpdateHealthBar(_health);

            if (_health <= 0)
            {
                // TODO: Test all circumstances where this is called.
                FighterDead();
            }
        }

        private void FighterDead()
        {
            ChangeFighterState(FighterState.Dead);
            
            _health = _fighterDataSo.Health;
            _healthUI.UpdateHealthBar(_health);
    
            _targetDestination = null;
    
            if (_isPlayerFighter)
            {
                gameObject.layer = LayerMask.NameToLayer(Const.Layers.PLAYER_FIGHTER);
                EventManager.OnPlayerFighterDead?.Invoke(this);
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer(Const.Layers.ENEMY_FIGHTER);
                EventManager.OnEnemyFighterDead?.Invoke(this); 
            }
        }

        #endregion

        #region AI Methods

        private void Move()
        {
            if (_targetDestination == null || _fighterState != FighterState.Move) return;
            _navMeshAgent.SetDestination(_targetDestination.position);

            if (Vector2.Distance(transform.position, _targetDestination.position) <= _navMeshAgent.stoppingDistance)
            {
                ChangeFighterState(FighterState.Attacking);
            }
        }

        private void FindNearestTarget()
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.1f, 1f), 0f, transform.right, 100f, _layerMask);
            if (hit.collider != null && hit.collider.transform != _targetDestination)
            {
                // If the hit collider is not the current target, set it as the new target
                if (hit.collider.TryGetComponent(out IAttackable attackable) && attackable != null)
                {
                    _targetDestination = hit.collider.transform;
                    ChangeFighterState(FighterState.Move);
                }
            }
        }

        #endregion

        #region Helper Methods

        private void ChangeFighterState(FighterState fighterState)
        {
            _fighterState = fighterState;
            _fighterAnimationController.SetAnimationState(_fighterState);
        }

        public void SetTargetDestination(Transform target)
        {
            _targetDestination = target;
        }

        private void Initialize()
        {
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
            _navMeshAgent.stoppingDistance = _attackRange;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _isPlaying = true;

                if (gameObject.activeInHierarchy)
                {
                    _navMeshAgent.isStopped = false;
                }
            }
            else if (gameState == GameState.Win || gameState == GameState.Lose)
            {
                FighterDead();
                _isPlaying = false;
                if (gameObject.activeInHierarchy)
                {
                    _navMeshAgent.isStopped = true;
                }
            }
            else
            {
                _isPlaying = false;
                if (gameObject.activeInHierarchy)
                {
                    _navMeshAgent.isStopped = true;
                }
            }
        }

        public FighterType GetFighterType()
        {
            return _fighterType;
        }

        #endregion
    }
}