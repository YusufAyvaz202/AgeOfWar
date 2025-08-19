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
        
        // This method is called from the animation event in the fighter's attack animation.
        public void Attack()
        {
            _baseFighter.Attack();
        }
    }
}
