using DG.Tweening;
using Economy;
using Managers;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        [Header("Meat Settings")]
        [SerializeField] private TextMeshProUGUI _meatProductionRateText;
        [SerializeField] private TextMeshProUGUI _meatProductionRateCostText;
        [SerializeField] private Button _increaseMeatProductionRateButton;
        
        [Header("Gold Settings")]
        [SerializeField] private TextMeshProUGUI _goldCountText;
        
        [Header("Animation References & Settings")]
        [SerializeField] private GameObject _blackBackgroundObject;
        [SerializeField] private GameObject _pausePopup;
        [SerializeField] private Button _startFightButton;

        [SerializeField] private float _animationDuration = 0.3f;
        private RectTransform _losePopupTransform;
        private RectTransform _winPopupTransform;
        private Image _blackBackgroundImage;


        #region Unity Methods

        private void Awake()
        {
            _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
            _winPopupTransform = _pausePopup.GetComponent<RectTransform>();

            _startFightButton.onClick.AddListener(StartFightButtonClick);
            
            _increaseMeatProductionRateButton.onClick.AddListener(IncreaseMeatProductionRateButtonClick);
            _startFightButton.onClick.AddListener(StartFightButtonClick);
        }

        #endregion
        
        public void UpdateMeatProductionRateText(float productionRate)
        {
            _meatProductionRateText.text = $"{productionRate:F2}/s";
        }
        
        public void UpdateMeatProductionRateCostText(int cost)
        {
            _meatProductionRateCostText.text = cost.ToString();
        }
        
        private void IncreaseMeatProductionRateButtonClick()
        {
            if (GoldManager.Instance.CanSpend(EconomyManager.Instance.GetMeatProductionRateCost()))
            {
                EconomyManager.Instance.IncreaseMeatProductionRate();
            }
        }

        private void StartFightButtonClick()
        {
            GameManager.Instance.SetGameState(GameState.Playing);
        }


        #region Animation Methods

        public void OnGameOver()
        {
            _blackBackgroundObject.SetActive(true);
            _pausePopup.SetActive(true);
            _startFightButton.gameObject.SetActive(true);

            _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
            _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
        }

        public void OnGameStart()
        {
            _blackBackgroundImage.DOFade(1f, _animationDuration).SetEase(Ease.Linear);
            _winPopupTransform.DOScale(1f, _animationDuration).SetEase(Ease.OutBack);

            _blackBackgroundObject.SetActive(false);
            _pausePopup.SetActive(false);
            _startFightButton.gameObject.SetActive(false);
        }

        #endregion
    }
}
