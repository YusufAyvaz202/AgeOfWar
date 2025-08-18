using Misc;
using UnityEngine;

namespace Fighter
{
    public class FighterAnimationController : MonoBehaviour
    {
        [Header("References")]
        private Animator _animator;

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion

        public void PlayMoveAnimation()
        {
            _animator.SetBool(Const.FighterAnimations.FIGHTER_MOVE, true);
            _animator.SetBool(Const.FighterAnimations.FIGHTER_ATTACK, false);
        }

        public void PlayAttackAnimation()
        {
            _animator.SetBool(Const.FighterAnimations.FIGHTER_MOVE, false);
            _animator.SetBool(Const.FighterAnimations.FIGHTER_ATTACK, true);
        }

        public void PlayIdleAnimation()
        {
            _animator.SetBool(Const.FighterAnimations.FIGHTER_MOVE, false);
            _animator.SetBool(Const.FighterAnimations.FIGHTER_ATTACK, false);
        }

        public void PlayDeadAnimation()
        {
            _animator.SetTrigger(Const.FighterAnimations.FIGHTER_DEAD);
        }
    }
}