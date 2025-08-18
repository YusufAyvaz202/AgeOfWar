using System.Collections;
using Abstracts;
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

        #region Unity Methods
        
        private void Start()
        {
            StartCoroutine(SpawnFighter());
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
    }
}