using Abstracts;
using Interfaces;
using Misc;
using UnityEngine;

namespace Fighter.Variants
{
    public class Ninja : BaseFighter
    {
        public override void Attack()
        {
            // TODO: This is same as the CaveMan attack method, in future we can add unique attack logic for Ninja. like Instantiate for throwing shurikens
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
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
}