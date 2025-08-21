using System.Collections;
using Abstracts;
using Misc;
using ObjectPooling;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class EnemyFighterSpawnManager : MonoBehaviour
    {

        [Header("References")] 
        [SerializeField] private BaseFighter[] _baseFighterPrefab;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _spawnParent;

        [Header("Settings")] 
        private Coroutine _spawnCoroutine;
        private ObjectPool _fighterPool;
        private int _currentSpawnCount;
        
        [Header("Level Settings")]
        [SerializeField] private LevelDataSO _currentLevelData;
        private Wave _currentWave;
        private float _timeBetweenEachFighter;
        private float _timeBetweenUnits;
        private int _currentWaveIndex;
        private bool _canSpawn;
        
        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
            EventManager.OnEnemyFighterDead += OnEnemyFighterDead;
        }

        private void Start()
        {
            _fighterPool = new ObjectPool(_baseFighterPrefab, 5, _spawnParent);
            LoadLevelData();
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
            EventManager.OnEnemyFighterDead -= OnEnemyFighterDead;
        }

        #endregion
        
        private IEnumerator SpawnFighters()
        {
            while (true)
            {
                foreach (var unit in _currentWave.Units)
                {
                    for (int i = 0; i < unit.UnitSpawnCount; i++)
                    {
                        SpawnUnit(unit);
                        yield return WaitForSecondsRealtimeCustom(_timeBetweenEachFighter, () => !_canSpawn);
                    }
                    yield return WaitForSecondsRealtimeCustom(unit.TimeBetweenEachUnits, () => !_canSpawn);
                }
                yield return WaitForSecondsRealtimeCustom(_currentWave.SpawnDelayBetweenWave, () => !_canSpawn);

                _currentWaveIndex++;
                if (_currentWaveIndex < _currentLevelData.Waves.Count)
                {
                    _currentWave = _currentLevelData.Waves[_currentWaveIndex];
                }
                else
                {
                    break;
                }
            }
        }

        private void SpawnUnit(Unit unit)
        {
            BaseFighter baseFighter = _fighterPool.GetFighter(unit.FighterType);
            baseFighter.transform.position = _spawnPosition.position;
        }

        #region Helper Methods
        
        private IEnumerator WaitForSecondsRealtimeCustom(float duration, System.Func<bool> pauseCondition)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                if (!pauseCondition())
                {
                    elapsed += Time.deltaTime;
                }
                yield return null;
            }
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _canSpawn = true;
                _spawnCoroutine ??= StartCoroutine(SpawnFighters());
            }
            else if (gameState == GameState.Win || gameState == GameState.Lose)
            {
                _canSpawn = false;
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
                
                LoadLevelData();
            }
            else
            {
                _canSpawn = false;
            }
        }
        
        private void LoadLevelData()
        {
            _currentWaveIndex = 0;
            _currentLevelData = LevelManager.Instance.GetLevelData();
            _currentWave = _currentLevelData.Waves[_currentWaveIndex];
            _timeBetweenEachFighter = _currentLevelData.TimeBetweenEachFighter;
        }
        
        private void OnEnemyFighterDead(BaseFighter baseFighter)
        {
            _fighterPool.ReturnFighterToPool(baseFighter);
        }

        #endregion
    }
}