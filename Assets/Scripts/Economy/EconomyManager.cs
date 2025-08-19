using System;
using System.Collections;
using Managers;
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

        private void Start()
        {
            _isProductionContinue = true;
            StartCoroutine(IncreaseMeat());
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
        
    }
}
