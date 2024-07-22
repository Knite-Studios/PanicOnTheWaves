using UnityEditor;
using World;

namespace Editor.Inspectors
{
    // [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var enemySpawner = target as EnemySpawner;
        }
    }
}