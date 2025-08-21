using System;
using Abstracts;
using Managers;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Economy
{
    public class GoldManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static GoldManager Instance;
        
        [Header("References")]
        private int _goldAmount;
        private float _minimumGoldAmount = 1f;
        private float _maximumGoldAmount = 3f;
        
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
            EventManager.OnEnemyFighterDead += IncreaseGoldAmount;
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
            EventManager.OnEnemyFighterDead -= IncreaseGoldAmount;
        }

        #endregion
        
        private void IncreaseGoldAmount(BaseFighter fighter)
        {
            if (!_isPlaying) return;
            _goldAmount += Convert.ToInt32(Random.Range(_minimumGoldAmount, _maximumGoldAmount));
            UIManager.Instance.UpdateGoldCountText(_goldAmount);
        }
        
        #region Helper Methods

        public void SetGoldAmount(int goldAmount)
        {
            _goldAmount = goldAmount;
            UIManager.Instance.UpdateGoldCountText(_goldAmount);
        }
        
        public int GetGoldAmount()
        {
            return _goldAmount;
        }
        
        public bool CanSpend(int goldCost)
        {
            if (_goldAmount >= goldCost)
            {
                _goldAmount -= goldCost;
                UIManager.Instance.UpdateGoldCountText(_goldAmount);
                return true;
            }
            return false;
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }

        #endregion
        
    }
}