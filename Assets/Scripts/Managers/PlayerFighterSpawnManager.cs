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
        [SerializeField] private BaseFighter _baseFighterPrefab;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _spawnPosition;

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

        #endregion

        public void SpawnCaveFighter()
        {
            if (EconomyManager.Instance.CanSpawn(Const.FighterCosts.CAVEMAN_COST))
            {
                BaseFighter baseFighter = Instantiate(_baseFighterPrefab);
                baseFighter.transform.position = _spawnPosition.position;
                baseFighter.SetTargetDestination(_target);
            }
        }
    }
}