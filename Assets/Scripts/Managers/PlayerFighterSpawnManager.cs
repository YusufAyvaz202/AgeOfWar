using Abstracts;
using Economy;
using Misc;
using UnityEngine;

namespace Managers
{
    public class PlayerFighterSpawnManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static PlayerFighterSpawnManager Instance;
        
        [Header("References")] 
        [SerializeField] private BaseFighter _caveManPrefab;
        [SerializeField] private BaseFighter _ninjaPrefab;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _spawnPosition;
        
        [Header("Settings")]
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

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }
        
        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion

        public void SpawnCaveFighter()
        {
            if (!_isPlaying) return;
            
            if (EconomyManager.Instance.CanSpawn(Const.FighterCosts.CAVEMAN_COST))
            {
                BaseFighter baseFighter = Instantiate(_caveManPrefab);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
        }
        
        public void SpawnNinjaFighter()
        {
            if (!_isPlaying) return;
            if (EconomyManager.Instance.CanSpawn(Const.FighterCosts.NINJA_COST))
            {
                BaseFighter baseFighter = Instantiate(_ninjaPrefab);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
        }

        #region Helper Methods

        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }

        #endregion
    }
}