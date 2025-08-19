using Abstracts;
using Interfaces;
using Misc;
using UnityEngine;

namespace Fighter.Variants
{
    public class CaveMan : BaseFighter
    {
        public override void Attack()
        {
            if(!_isPlaying) return;
            if (_fighterState == FighterState.Attacking)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _attackRange, _layerMask);
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out IAttackable attackable))
                    {
                        attackable.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}