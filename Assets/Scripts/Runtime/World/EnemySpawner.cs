using System.Collections.Generic;
using Managers;
using NaughtyAttributes;
using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World
{
    [DefaultExecutionOrder(200)]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GridBehaviour grid;
        [SerializeField] private List<EnemyInfo> enemies = new List<EnemyInfo>();
        [SerializeField] private bool randomSpawn;
        [SerializeField] private float spawnInterval = 3.0f;

        private List<Transform> _spawnPoints;
        private float _timer;

        private void Start()
        {
            SetupSpawnPoints();
        }

        private void Update()
        {
            if (!randomSpawn) return;

            _timer += Time.deltaTime;
            if (!(_timer >= spawnInterval)) return;

            SpawnRandomEnemy();
            _timer = 0;
        }

        private void SetupSpawnPoints()
        {
            _spawnPoints = new List<Transform>();
            for (var i = 0; i < grid.Rows; i++)
            {
                var spawnPoint = new GameObject($"SpawnPoint_{i}").transform;
                var cellCenter = grid.GetCellTopCenter(0, i);
                // Debug.Log($"Cell Center: {cellCenter}");

                spawnPoint.position = new Vector3(transform.position.x, transform.position.y,
                    cellCenter.z);
                spawnPoint.parent = transform;
                _spawnPoints.Add(spawnPoint);
            }
        }

        public void SpawnEnemy(EnemyInfo enemyInfo, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _spawnPoints.Count) return;

            var spawnPosition = _spawnPoints[rowIndex].position;
            PrefabManager.Create(enemyInfo.prefab, spawnPosition);
        }

        [Button]
        private void SpawnRandomEnemy()
        {
            if (enemies.Count == 0) return;

            var randomEnemy = enemies[Random.Range(0, enemies.Count)];
            var randomRow = Random.Range(0, _spawnPoints.Count);
            SpawnEnemy(randomEnemy, randomRow);
        }
    }
}