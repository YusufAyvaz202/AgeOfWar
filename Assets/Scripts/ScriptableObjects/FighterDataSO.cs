using Misc;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObject/Enemy")]
    public class FighterDataSO: ScriptableObject
    {
        public FighterType FighterType;
        public float AttackRange;
        public float MoveSpeed;
        public float Health;
        public float Damage;
    }
}