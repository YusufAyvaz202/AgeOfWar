using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerSpawnUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button _caveManSpawnButton;
        [SerializeField] private Button _ninjaSpawnButton;

        #region Unity Methods

        private void Awake()
        {
            _caveManSpawnButton.onClick.AddListener(SpawnCaveManButtonClick);
            _ninjaSpawnButton.onClick.AddListener(SpawnNinjaButtonClick);
        }

        #endregion

        private void SpawnCaveManButtonClick()
        {
            PlayerFighterSpawnManager.Instance.SpawnCaveFighter();
        }

        private void SpawnNinjaButtonClick()
        {
            PlayerFighterSpawnManager.Instance.SpawnNinjaFighter();
        }
    }
}