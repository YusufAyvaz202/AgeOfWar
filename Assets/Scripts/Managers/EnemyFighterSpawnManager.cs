using System.Collections;
using Abstracts;
using Misc;
using UnityEngine;

namespace Managers
{
    public class EnemyFighterSpawnManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private BaseFighter _baseFighterPrefab;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private bool _isSpawning = true;
        [SerializeField] private float _timeBetweenSpawns = 0.2f;
        
        [Header("Settings")]
        private Coroutine _spawnCoroutine;

        #region Unity Methods
        
        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion

        IEnumerator SpawnFighter()
        {
            while (_isSpawning)
            {
                yield return new WaitForSeconds(_timeBetweenSpawns);
                BaseFighter baseFighter = Instantiate(_baseFighterPrefab);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
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