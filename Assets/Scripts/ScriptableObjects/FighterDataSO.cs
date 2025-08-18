using Misc;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObject/Enemy")]
    public class FighterDataSO: ScriptableObject
    {
        public FighterType _fighterType;
        public float AttackSpeed;
        public float MoveSpeed;
        public float Health;
        public float Damage;
    }
}