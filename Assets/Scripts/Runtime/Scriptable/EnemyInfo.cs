using UnityEngine;
namespace Scriptable
{
    [CreateAssetMenu(menuName = "POTW/Enemy Info", fileName = "Enemy Info")]
    public class EnemyInfo : ScriptableObject
    {
        public PrefabType prefab;
        public string enemyName;
    }
}