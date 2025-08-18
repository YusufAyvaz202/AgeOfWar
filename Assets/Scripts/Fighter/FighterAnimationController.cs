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

        private void PlayMoveAnimation()
        {
            _animator.SetBool(Const.FighterAnimations.FIGHTER_MOVE, true);
            _animator.SetBool(Const.FighterAnimations.FIGHTER_ATTACK, false);
        }

        private void PlayAttackAnimation()
        {
            _animator.SetBool(Const.FighterAnimations.FIGHTER_MOVE, false);
            _animator.SetBool(Const.FighterAnimations.FIGHTER_ATTACK, true);
        }

        private void PlayIdleAnimation()
        {
            _animator.SetBool(Const.FighterAnimations.FIGHTER_MOVE, false);
            _animator.SetBool(Const.FighterAnimations.FIGHTER_ATTACK, false);
        }

        private void PlayDeadAnimation()
        {
            _animator.SetTrigger(Const.FighterAnimations.FIGHTER_DEAD);
        }

        public void SetAnimationState(FighterState fighterState)
        {
            switch (fighterState)
            {
                case FighterState.Waiting:
                    PlayIdleAnimation();
                    break;
                case FighterState.Move:
                    PlayMoveAnimation();
                    break;
                case FighterState.Attacking:
                    PlayAttackAnimation();
                    break;
                case FighterState.Dead:
                    PlayDeadAnimation();
                    break;
            }
        }
    }
}