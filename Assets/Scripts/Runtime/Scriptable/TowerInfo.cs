using UnityEngine;

namespace Scriptable
{
    namespace Scriptable
    {
        [CreateAssetMenu(menuName = "POTW/Tower Info", fileName = "Tower Info")]
        public class TowerInfo : ScriptableObject
        {
            public PrefabType towerPrefab;
            public int cost;
        }
    }
}