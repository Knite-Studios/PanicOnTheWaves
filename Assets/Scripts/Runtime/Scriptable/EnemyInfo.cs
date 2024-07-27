using Common.Attributes;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "POTW/Enemy Info", fileName = "Enemy Info")]
    public class EnemyInfo : ScriptableObject
    {
        public PrefabType prefab;

        public int cost;
        public int weight;

        [TitleHeader("Enemy Stats")]
        public int health;
        public int damage;
        public float speed;
    }
}