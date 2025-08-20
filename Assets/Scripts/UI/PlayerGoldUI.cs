using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerGoldUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _goldCountText;

        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnGoldAmountChanged += UpdateGoldCountText;
        }

        private void OnDisable()
        {
            EventManager.OnGoldAmountChanged -= UpdateGoldCountText;
        }

        #endregion
        
        private void UpdateGoldCountText(int goldCount)
        {
            _goldCountText.text = goldCount.ToString();   
        }
    }
}