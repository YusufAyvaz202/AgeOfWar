using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerMeatInfoUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _meatCountText;

        public void UpdateMeatCountText(int meatCount)
        {
            _meatCountText.text = meatCount.ToString();   
        }
    }
}
