using System.Collections;
using Abstracts;
using Misc;
using ObjectPooling;
using UnityEngine;

namespace Managers
{
    public class EnemyFighterSpawnManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private BaseFighter[] _baseFighterPrefab;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private bool _isSpawning = true;
        [SerializeField] private float _timeBetweenSpawns = 0.2f;
        
        [Header("Settings")]
        private Coroutine _spawnCoroutine;
        private ObjectPool _fighterPool;

        #region Unity Methods
        
        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
            EventManager.OnEnemyFighterDead += OnEnemyFighterDead;
        }

        private void Start()
        {
            _fighterPool = new ObjectPool(_baseFighterPrefab, 5, _spawnParent);
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
            EventManager.OnEnemyFighterDead -= OnEnemyFighterDead;
        }

        #endregion

        private IEnumerator SpawnFighter()
        {
            while (_isSpawning)
            {
                yield return new WaitForSeconds(_timeBetweenSpawns);
                BaseFighter baseFighter = _fighterPool.GetFighter(FighterType.CaveMan);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
        }
        
        private void OnEnemyFighterDead(BaseFighter baseFighter)
        {
            _fighterPool.ReturnFighterToPool(baseFighter);
        }

        #region Helper Methods

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _isSpawning = true;
                _spawnCoroutine = StartCoroutine(SpawnFighter());
            }
            else
            {
                _isSpawning = false;
                StopCoroutine(_spawnCoroutine);
            }
        }

        #endregion
        
    }
}