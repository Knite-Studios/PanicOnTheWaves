using System;
using System.Collections.Generic;
using System.Linq;
using Common.Attributes;
using Entity.Enemies;
using Managers;
using NaughtyAttributes;
using Scriptable.Scriptable;
using Systems.Attributes;
using UnityEngine;
using UnityEngine.Events;
using World;
using World.Projectiles;

namespace Entity.Towers
{
    public class BaseTower : BaseEntity
    {
        [TitleHeader("Base Tower Settings")]
        [SerializeField, Required] private TowerInfo info;
        [SerializeField, Required] private Transform spawnPoint;
        [SerializeField] private float attackInterval = 1.0f;

        private float _attackTimer;

        #region Attribute Getters
        
        public int AttackRange => this.GetAttributeValue<int>(GameAttribute.AttackRange);
        
        #endregion

        public event UnityAction OnTowerDestroyed;

        private void Update()
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0)
            {
                Attack();
                _attackTimer = attackInterval;
            }
        }

        private void OnDestroy()
        {
            OnTowerDestroyed?.Invoke();
            
            // Unsubscribe to all handlers.
            foreach (var d in OnTowerDestroyed!.GetInvocationList())
            {
                OnTowerDestroyed -= (UnityAction) d;
            }
        }

        protected override void ApplyBaseAttributes()
        {
            this.GetOrCreateAttribute(GameAttribute.MaxHealth, info.health);
            this.GetOrCreateAttribute(GameAttribute.Damage, info.damage);
            this.GetOrCreateAttribute(GameAttribute.AttackRange, info.attackRange);
            
            CurrentHealth = MaxHealth;
        }

        public override void Attack()
        {
            var gridPosition = Grid.GetGridPosition(transform.position);
            var cellsInRange = Grid.GetCellsInRowRange(gridPosition, AttackRange);

            if (cellsInRange
                .Select(FindEnemiesInCell)
                .Any(enemies => enemies.Count > 0))
            {
                Shoot();
            }
        }

        private List<BaseEnemy> FindEnemiesInCell(GridBehaviour.Cell cell)
        {
            var enemies = new List<BaseEnemy>();
            var results = new Collider[10];
            var size = Physics.OverlapSphereNonAlloc(cell.Center, 0.5f, results);
            for (var i = 0; i < size; i++)
            {
                if (results[i].TryGetComponent(out BaseEnemy enemy))
                    enemies.Add(enemy);
            }

            return enemies;
        }
        
        private void Shoot()
        {
            var projectile = PrefabManager.Create<BaseProjectile>(PrefabType.Projectile, spawnPoint.position);
            projectile.SetAttributes(Damage, this);
        }
    }
}