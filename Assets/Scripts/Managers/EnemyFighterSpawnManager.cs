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
        [SerializeField] private LevelDataSO _currentLevelData;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _spawnParent;

        [Header("Settings")] 
        private Coroutine _spawnCoroutine;
        private ObjectPool _fighterPool;
        private int _currentWaveIndex;
        private int _currentUnitIndex;
        private int _currentSpawnCount;
        private bool _isPaused;

        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
            EventManager.OnEnemyFighterDead += OnEnemyFighterDead;
        }

        private void Start()
        {
            _fighterPool = new ObjectPool(_baseFighterPrefab, 5, _spawnParent);
            _currentLevelData = LevelManager.Instance.GetLevelData();
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
            EventManager.OnEnemyFighterDead -= OnEnemyFighterDead;
        }

        #endregion
        
        private IEnumerator SpawnFighter()
        {
            for (int w = _currentWaveIndex; w < _currentLevelData.Waves.Count; w++)
            {
                var wave = _currentLevelData.Waves[w];
                for (int u = _currentUnitIndex; u < wave.Units.Count; u++)
                {
                    var unit = wave.Units[u];
                    for (int i = _currentSpawnCount; i < unit.UnitSpawnCount; i++)
                    {
                        BaseFighter baseFighter = _fighterPool.GetFighter(unit.FighterType);
                        baseFighter.transform.position = _spawnPosition.position;

                        _currentSpawnCount++;

                        yield return WaitForSecondsRealtimeCustom(_currentLevelData.TimeBetweenEachFighter, () => _isPaused);
                    }

                    _currentUnitIndex = u + 1;
                    _currentSpawnCount = 0;

                    yield return WaitForSecondsRealtimeCustom(unit.TimeBetweenEachUnits, () => _isPaused);
                }

                _currentWaveIndex = w + 1;
                _currentUnitIndex = 0;

                yield return WaitForSecondsRealtimeCustom(wave.SpawnDelayBetweenWawe, () => _isPaused);
            }

            _spawnCoroutine = null;
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
                _isPaused = false;
                _spawnCoroutine ??= StartCoroutine(SpawnFighter());
            }
            else if (gameState == GameState.Win || gameState == GameState.Lose)
            {
                _isPaused = true;

                if (_spawnCoroutine != null)
                {
                    StopCoroutine(_spawnCoroutine);
                }
                
                _spawnCoroutine = null;
                _currentSpawnCount = 0;
                _currentUnitIndex = 0;
                _currentWaveIndex = 0;
                
                _currentLevelData = LevelManager.Instance.GetLevelData();
            }
            else
            {
                _isPaused = true;
            }
        }
        
        private void OnEnemyFighterDead(BaseFighter baseFighter)
        {
            _fighterPool.ReturnFighterToPool(baseFighter);
        }

        #endregion
    }
}