using Entity.Enemies;
using UnityEngine;

namespace Entity.StateMachines.Enemy
{
    public class EnemyBaseState : BaseState<BaseEntity>
    {
        protected readonly BaseEnemy Enemy;

        private float _actionTimer;
        private bool IsActionReady => _actionTimer <= 0;

        public EnemyBaseState(string name, BaseEntity owner) : base(name, owner)
        {
            Enemy = owner as BaseEnemy;
            _actionTimer = Enemy!.ActionDelay;
        }

        public override void UpdateState()
        {
            if (!IsActionReady)
            {
                _actionTimer -= Time.deltaTime;
                return;
            }
            
            // Check for towers in the current cell.
            var gridPosition = Enemy.Grid.GetGridPosition(Enemy.transform.position);
            var currentCell = Enemy.Grid.GetCell(gridPosition);

            // If there's a tower in the current cell, attack it. Otherwise, move.
            Enemy.ChangeState(currentCell != null && currentCell.OccupyingTower
                ? Enemy.AttackState
                : Enemy.MoveState);
        }
    }
}