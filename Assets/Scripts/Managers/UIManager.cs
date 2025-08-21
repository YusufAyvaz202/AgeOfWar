using Misc;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Singleton")] 
        public static UIManager Instance;

        [Header("References")]
        private PlayerMeatInfoUI _playerMeatInfoUI;
        private PlayerGoldUI _playerGoldUI;
        private ShopUI _shopUI;

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

            _playerMeatInfoUI = FindObjectOfType<PlayerMeatInfoUI>();
            _playerGoldUI = FindObjectOfType<PlayerGoldUI>();
            _shopUI = FindObjectOfType<ShopUI>();
            
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

        #region PlayerMeatInfo Methods

        public void UpdateMeatCountText(int meatCount)
        {
            _playerMeatInfoUI.UpdateMeatCountText(meatCount);
        }
        
        public void UpdateMeatProductionRateText(float meatProduction)
        {
            _playerMeatInfoUI.UpdateMeatProductionRateText(meatProduction);
            _shopUI.UpdateMeatProductionRateText(meatProduction);
        }

        #endregion

        #region PlayerGold Methods

        public void UpdateGoldCountText(int goldCount)
        {
            _playerGoldUI.UpdateGoldCountText(goldCount);
        }

        #endregion

        #region ShopUI Methods
        
        private void OnGameOver()
        {
            _shopUI.OnGameOver();
        }
        
        private void OnGameStart()
        {
            _shopUI.OnGameStart();
        }
        
        public void UpdateIncreaseMeatProductionRateText(int amount)
        {
            _shopUI.UpdateMeatProductionRateCostText(amount);
        }
        
        #endregion

        #region Helper Methods

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Lose || gameState == GameState.Win || gameState == GameState.Waiting)
            {
                OnGameOver();
            }
            else if (gameState == GameState.Playing)
            {
                OnGameStart();
            }
        }

        #endregion
    }
}