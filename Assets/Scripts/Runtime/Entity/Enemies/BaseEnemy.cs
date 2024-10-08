﻿using System.Collections.Generic;
using Common.Attributes;
using Entity.StateMachines;
using Entity.StateMachines.Enemy;
using NaughtyAttributes;
using Scriptable;
using Systems.Attributes;
using UnityEngine;

namespace Entity.Enemies
{
    public class BaseEnemy : BaseEntity
    {
        [TitleHeader("Base Enemy Settings")]
        [SerializeField, Required] private EnemyInfo info;

        #region Statemachine References
        
        internal EnemyIdleState IdleState;
        internal EnemyMoveState MoveState;
        internal EnemyAttackState AttackState;
        
        #endregion

        private BaseState<BaseEntity> _currentState;

        #region Attribute Getters
        
        public float Speed => this.GetAttributeValue<float>(GameAttribute.Speed);
        public float ActionDelay => this.GetAttributeValue<float>(GameAttribute.ActionDelay);
        
        #endregion

        public BaseState<BaseEntity> State => _currentState;
        public List<Vector3> PathWaypoints { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            // Initialize the enemy states.
            InitializeStates();
            ChangeState(IdleState);
        }

        protected virtual void Update()
        {
            _currentState?.UpdateState();
        }

        protected virtual void FixedUpdate()
        {
            _currentState?.FixedUpdateState();
        }

        private void InitializeStates()
        {
            IdleState = new EnemyIdleState("Idle", this);
            MoveState = new EnemyMoveState("Move", this);
            AttackState = new EnemyAttackState("Attack", this);
        }

        protected override void ApplyBaseAttributes()
        {
            this.GetOrCreateAttribute(GameAttribute.MaxHealth, info.health);
            this.GetOrCreateAttribute(GameAttribute.Damage, info.damage);
            this.GetOrCreateAttribute(GameAttribute.Speed, info.speed);
            this.GetOrCreateAttribute(GameAttribute.ActionDelay, info.actionDelay);

            CurrentHealth = MaxHealth;
        }

        public void ChangeState(EnemyBaseState state)
        {
            _currentState?.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }
        
        public void SetPath(List<Vector3> path)
        {
            path.Reverse();
            PathWaypoints = path;
        }

        public override void Attack()
        {
            var gridPosition = Grid.GetGridPosition(transform.position);
            var currentCell = Grid.GetCell(gridPosition);
            var targetTower = currentCell?.OccupyingTower;
            targetTower?.TakeDamage(Damage);
        }
    }
}