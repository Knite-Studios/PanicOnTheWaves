using System.Collections.Generic;
using Common.Attributes;
using Entity.StateMachines;
using Entity.StateMachines.Enemy;
using Scriptable;
using Systems.Attributes;
using UnityEngine;

namespace Entity.Enemies
{
    public class BaseEnemy : BaseEntity
    {
        [TitleHeader("Base Enemy Settings")]
        [SerializeField] private EnemyInfo info;
        [Tooltip("The delay before the enemy performs an action.")]
        public float actionDelay = 1.0f;

        #region Statemachine References
        
        internal EnemyIdleState IdleState;
        internal EnemyMoveState MoveState;
        internal EnemyAttackState AttackState;
        
        #endregion

        private BaseState<BaseEntity> _currentState;

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
    }
}