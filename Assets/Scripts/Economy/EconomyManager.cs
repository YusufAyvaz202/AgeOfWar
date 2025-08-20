using System.Collections;
using Managers;
using Misc;
using UnityEngine;

namespace Economy
{
    public class EconomyManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static EconomyManager Instance;
        
        [Header("Economy Settings")]
        public float _meatCount;
        public float _meatProductionPerTime;
        private bool _isProductionContinue;
        private Coroutine _productionCoroutine;

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
        
        private IEnumerator IncreaseMeat()
        {
            while (_isProductionContinue)
            {
                //Increase Meat count per time
                yield return new WaitForSeconds(1f);
                _meatCount += _meatProductionPerTime;
                
                // Update Meat Count UI.
                UIManager.Instance.UpdateMeatCountText(Mathf.FloorToInt(_meatCount));
            }
        }

        #region Helper Methods

        public bool CanSpawn(int meatCost)
        {
            if (_meatCount >= meatCost)
            {
                _meatCount -= meatCost;
                UIManager.Instance.UpdateMeatCountText(Mathf.FloorToInt(_meatCount));
                return true;
            }
            return false;
        }
        
        public float GetCurrentMeatProduction()
        {
            return _meatProductionPerTime;
        }
        
        public void SetMeatProduction(float production)
        {
            _meatProductionPerTime = production;
            //UIManager.Instance.UpdateMeatProductionText(Mathf.FloorToInt(_meatProductionPerTime));
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _isProductionContinue = true;
                _productionCoroutine = StartCoroutine(IncreaseMeat());
            }
            else
            {
                _isProductionContinue = false;
                StopCoroutine(_productionCoroutine);
            }
        }

        #endregion
        
    }
}
