using UnityEngine;

namespace Scriptable
{
    namespace Scriptable
    {
        [CreateAssetMenu(menuName = "POTW/Tower Info", fileName = "Tower Info")]
        public class TowerInfo : ScriptableObject
        {
            [Header("Tower Management")]
            public PrefabType towerPrefab;
            public int cost;
            public float cooldown = 2.0f;

            [Header("Tower Stats")]
            public int health;
            public int damage;
            public int attackRange;
        }
    }
}