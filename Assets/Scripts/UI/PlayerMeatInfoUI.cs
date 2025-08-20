using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerMeatInfoUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _meatCountText;
        [SerializeField] private TextMeshProUGUI _meatProductionRateText;

        public void UpdateMeatCountText(int meatCount)
        {
            _meatCountText.text = meatCount.ToString();   
        }
        
        public void UpdateMeatProductionRateText(float productionRate)
        {
            _meatProductionRateText.text = $"{productionRate:F2}/s";
        }
    }
}
