using Abstracts;
using Economy;
using Misc;
using ObjectPooling;
using UnityEngine;

namespace Managers
{
    public class PlayerFighterSpawnManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static PlayerFighterSpawnManager Instance;
        
        [Header("References")] 
        [SerializeField] private BaseFighter[] _baseFighterPrefabs;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _spawnParent;
        
        [Header("Settings")]
        private ObjectPool _fighterPool;
        private bool _isPlaying;

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

        private void Start()
        {
            _fighterPool = new ObjectPool(_baseFighterPrefabs, 5, _spawnParent);
        }

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
            EventManager.OnPlayerFighterDead += OnEnemyFighterDead;
        }
        
        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
            EventManager.OnPlayerFighterDead -= OnEnemyFighterDead;
        }

        #endregion

        public void SpawnCaveFighter()
        {
            if (!_isPlaying) return;
            
            if (EconomyManager.Instance.CanSpawn(Const.FighterCosts.CAVEMAN_COST))
            {
                BaseFighter baseFighter = _fighterPool.GetFighter(FighterType.CaveMan);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
        }
        
        public void SpawnNinjaFighter()
        {
            if (!_isPlaying) return;
            if (EconomyManager.Instance.CanSpawn(Const.FighterCosts.NINJA_COST))
            {
                BaseFighter baseFighter = _fighterPool.GetFighter(FighterType.Ninja);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
        }

        #region Helper Methods

        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }
        
        private void OnEnemyFighterDead(BaseFighter baseFighter)
        {
            _fighterPool.ReturnFighterToPool(baseFighter);
        }

        #endregion
    }
}