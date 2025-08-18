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
        private bool _isSpawning = true;
        
        private void Start()
        {
            StartCoroutine(SpawnFighter());
        }

        IEnumerator SpawnFighter()
        {
            while (_isSpawning)
            {
                yield return new WaitForSeconds(5f);
                BaseFighter baseFighter = Instantiate(_baseFighterPrefab);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
        }
    }
}