using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerGoldUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _goldCountText;

        public void UpdateGoldCountText(int goldCount)
        {
            _goldCountText.text = goldCount.ToString();   
        }
    }
}