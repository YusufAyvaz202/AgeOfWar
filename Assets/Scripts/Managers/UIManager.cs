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
        }

        #endregion

        #region PlayerMeatInfo Methods

        public void UpdateMeatCountText(int meatCount)
        {
            _playerMeatInfoUI.UpdateMeatCountText(meatCount);
        }

        #endregion

        #region PlayerGold Methods

        public void UpdateGoldCountText(int goldCount)
        {
            _playerGoldUI.UpdateGoldCountText(goldCount);
        }

        #endregion
    }
}