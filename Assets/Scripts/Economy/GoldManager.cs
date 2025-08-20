using System;
using Abstracts;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Economy
{
    public class GoldManager : MonoBehaviour
    {
        [Header("References")]
        private int _goldAmount;
        private float _minimumGoldAmount = 1f;
        private float _maximumGoldAmount = 3f;

        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnEnemyFighterDead += IncreaseGoldAmount;
        }

        #endregion
        
        private void IncreaseGoldAmount(BaseFighter fighter)
        {
            _goldAmount +=Convert.ToInt32(Random.Range(_minimumGoldAmount, _maximumGoldAmount));
            EventManager.OnGoldAmountChanged?.Invoke(_goldAmount);
        }
        
        public bool CanSpend(int meatCost)
        {
            if (_goldAmount >= meatCost)
            {
                _goldAmount -= meatCost;
                EventManager.OnGoldAmountChanged?.Invoke(_goldAmount);
                return true;
            }
            return false;
        }
    }
}