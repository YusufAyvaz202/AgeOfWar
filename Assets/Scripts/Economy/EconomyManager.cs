using System;
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
        private float _meatCount;
        private float _meatProductionRate;
        private int _meatProductionRateCost = 2;
        private float _meatProductionRateIncreaseRate = 0.2f;
        private float _meatProductionRateIncreaseCostRate = 1.5f;
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
                _meatCount += _meatProductionRate;
                
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
        
        public float GetCurrentMeatProductionRate()
        {
            return _meatProductionRate;
        }
        
        public void SetMeatProductionRate(float production)
        {
            _meatProductionRate = production;
            UIManager.Instance.UpdateMeatProductionRateText(_meatProductionRate);
        }
        
        public void IncreaseMeatProductionRate()
        {
            _meatProductionRate += _meatProductionRateIncreaseRate;
            UIManager.Instance.UpdateMeatProductionRateText(_meatProductionRate);
            
            IncreaseMeatProductionRateCost();
        }

        public int GetMeatProductionRateCost()
        {
            return _meatProductionRateCost;
        }

        public void SetMeatProductionRateCost(int cost)
        {
            _meatProductionRateCost = cost;
            UIManager.Instance.UpdateIncreaseMeatProductionRateText(_meatProductionRateCost);
        }

        private void IncreaseMeatProductionRateCost()
        {
            _meatProductionRateCost = (int)MathF.Ceiling(_meatProductionRateCost * _meatProductionRateIncreaseCostRate);
            UIManager.Instance.UpdateIncreaseMeatProductionRateText(_meatProductionRateCost);
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _isProductionContinue = true;
                _productionCoroutine = StartCoroutine(IncreaseMeat());
            }
            else if (gameState == GameState.Win || gameState == GameState.Lose)
            {
                _isProductionContinue = false;
                
                if (_productionCoroutine is not null)
                {
                    StopCoroutine(_productionCoroutine);
                }
                
                _meatCount = 0;
                UIManager.Instance.UpdateMeatCountText(Mathf.FloorToInt(_meatCount));
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
