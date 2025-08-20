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
        public int _meatProductionPerTimeCost;
        private bool _isProductionContinue;
        
        [Header("Other Settings")]
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
        
        public void SetMeatProductionRate(float production)
        {
            _meatProductionPerTime = production;
            UIManager.Instance.UpdateMeatProductionRateText(_meatProductionPerTime);
        }
        
        public void IncreaseMeatProductionRate()
        {
            _meatProductionPerTime += 0.2f;
            UIManager.Instance.UpdateMeatProductionRateText(_meatProductionPerTime);
        }

        public int IncreaseMeatProductionRateCost()
        {
            return _meatProductionPerTimeCost;
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
                
                if (_productionCoroutine is not null)
                {
                    StopCoroutine(_productionCoroutine);
                }
            }
        }

        #endregion
        
    }
}
