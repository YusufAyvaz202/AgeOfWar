using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerSpawnUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button _caveManSpawnButton;

        #region Unity Methods

        private void Awake()
        {
            _caveManSpawnButton.onClick.AddListener(SpawnCaveManButtonClick);
        }

        #endregion

        private void SpawnCaveManButtonClick()
        {
            PlayerFighterSpawnManager.Instance.SpawnCaveFighter();
        }
    }
}