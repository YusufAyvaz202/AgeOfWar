using Abstracts;
using UnityEngine;

namespace Fighter
{
    public class FighterAnimationEventController : MonoBehaviour
    {
        private BaseFighter _baseFighter;

        #region Unity Methods

        private void Awake()
        {
            _baseFighter = GetComponentInParent<BaseFighter>();
        }

        #endregion
        
        public void Attack()
        {
            _baseFighter.Attack();
        }
    }
}
