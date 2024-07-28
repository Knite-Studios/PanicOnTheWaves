﻿using System;
using System.Collections.Generic;
using Entity.Enemies;
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

        private List<Transform> _spawnPoints = new();

        private void Start()
        {
            SetupSpawnPoints();
        }

        private void OnEnable()
            => GameManager.OnWaveStart += HandleWaveStart;
        
        private void OnDisable()
            => GameManager.OnWaveStart -= HandleWaveStart;

        private void SetupSpawnPoints()
        {
            for (var i = 0; i < grid.Rows; i++)
            {
                var spawnPoint = new GameObject($"SpawnPoint_{i}").transform;
                var cellTopCenter = grid.GetCellTopCenter(0, i);
                // Debug.Log($"Cell Center: {cellCenter}");
                // Adjust the spawn point position to be at the top surface.
                spawnPoint.position = new Vector3(transform.position.x, cellTopCenter.y, cellTopCenter.z);
                spawnPoint.parent = transform;
                _spawnPoints.Add(spawnPoint);
            }
        }

        private void HandleWaveStart(List<EnemyInfo> wave)
        {
            foreach (var enemy in wave)
            {
                SpawnEnemy(enemy, Random.Range(0, _spawnPoints.Count));
            }
        }

        private void SpawnEnemy(EnemyInfo enemyInfo, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _spawnPoints.Count) return;

            var spawnPosition = _spawnPoints[rowIndex].position;
            var enemy = PrefabManager.Create<BaseEnemy>(enemyInfo.prefab, spawnPosition);
            enemy.SetPath(grid.GetRowWaypoints(rowIndex));
        }

        [Button]
        public void SpawnRandomEnemy()
        {
            var enemies = WaveManager.Instance.enemies;
            if (enemies.Count == 0) return;

            var randomEnemy = enemies[Random.Range(0, enemies.Count)];
            var randomRow = Random.Range(0, _spawnPoints.Count);
            SpawnEnemy(randomEnemy, randomRow);
        }
    }
}