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
        }

        #endregion

        #region PlayerMeatInfo Methods

        public void UpdateMeatCountText(int meatCount)
        {
            _playerMeatInfoUI.UpdateMeatCountText(meatCount);
        }

        #endregion
    }
}