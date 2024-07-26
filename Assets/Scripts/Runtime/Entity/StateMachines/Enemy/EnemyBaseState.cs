using Entity.Enemies;
using UnityEngine;

namespace Entity.StateMachines.Enemy
{
    public class EnemyBaseState : BaseState<BaseEntity>
    {
        protected readonly BaseEnemy Enemy;

        private float _actionTimer;
        private bool _isActionReady => _actionTimer <= 0;

        public EnemyBaseState(string name, BaseEntity owner) : base(name, owner)
        {
            Enemy = owner as BaseEnemy;
            _actionTimer = Enemy!.actionDelay;
        }

        public override void UpdateState()
        {
            if (!_isActionReady)
            {
                _actionTimer -= Time.deltaTime;
                return;
            }
            
            // If we're ready to perform an action, we handle switching to Move or Attack states.
            // For now we'll just switch to MoveState.
            Enemy.ChangeState(Enemy.MoveState);
        }
    }
}