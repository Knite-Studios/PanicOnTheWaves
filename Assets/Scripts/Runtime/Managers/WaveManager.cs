using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Attributes;
using NaughtyAttributes;
using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    [InitializeSingleton]
    public class WaveManager : MonoSingleton<WaveManager>
    {
        /// <summary>
        /// Special singleton initializer method.
        /// </summary>
        public new static void Initialize()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Managers/WaveManager");
            if (prefab == null) throw new Exception("Missing WaveManager prefab!");

            var instance = Instantiate(prefab);
            if (instance == null) throw new Exception("Failed to instantiate WaveManager prefab!");

            instance.name = "Managers.WaveManager (MonoSingleton)";
        }

        public List<EnemyInfo> enemies = new();
        [SerializeField] private int basePoints = 100;
        [SerializeField] private ScalingType scalingType = ScalingType.Custom;
        [ShowIf("scalingType", ScalingType.Exponential)]
        [SerializeField] private float difficultyMultiplier = 1.2f;
        [ShowIf("scalingType", ScalingType.Custom)]
        [SerializeField] private float scalingFactor = 10.0f;
        
        private int _currentWave = 1;
        private int _totalPoints;

        protected override void Awake()
        {
            base.Awake();

            CalculateTotalPoints();
        }
        
        private void CalculateTotalPoints()
        {
            _totalPoints = scalingType switch
            {
                ScalingType.Linear => basePoints * _currentWave,
                ScalingType.Exponential => Mathf.RoundToInt(basePoints * Mathf.Pow(difficultyMultiplier, _currentWave - 1)),
                ScalingType.Custom => Mathf.RoundToInt(basePoints * (1 + (_currentWave - 1) / scalingFactor)),
                _ => _totalPoints
            };
        }

        public List<EnemyInfo> GetNextWave()
        {
            var selectedEnemies = new List<EnemyInfo>();
            var points = _totalPoints;
            
            while (points > 0)
            {
                // If we can't afford any enemy, break the loop.
                var enemy = SelectEnemy(points);
                if (enemy == null) break;
                
                selectedEnemies.Add(enemy);
                points -= enemy.cost;
            }
            
            _currentWave++;
            CalculateTotalPoints();

            return selectedEnemies;
        }

        private EnemyInfo SelectEnemy(int pointsLeft)
        {
            // Randomly select an enemy based on their weight.
            var affordableEnemies = enemies.FindAll(enemy => enemy.cost <= pointsLeft);
            if (affordableEnemies.Count == 0) return null;

            // Tallies the total weight of all affordable enemies.
            var totalWeight = affordableEnemies.Sum(enemy => enemy.weight);
            
            // Iterate through the affordable enemies and select one based on the random weight.
            var randomWeight = Random.Range(0, totalWeight);
            foreach (var enemy in affordableEnemies)
            {
                if (randomWeight < enemy.weight) return enemy;
                randomWeight -= enemy.weight;
            }
            
            return null;
        }

        private enum ScalingType
        {
            Linear,
            Exponential,
            Custom
        }
    }
}