using Entity.Towers;

namespace Entity.StateMachines.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private BaseTower _targetTower;

        public EnemyAttackState(string name, BaseEntity owner) : base(name, owner)
        {
        }
        
        public override void EnterState()
        {
            // Animation logic here.
            
            // Get the tower in the current cell.
            var gridPosition = Enemy.Grid.GetGridPosition(Enemy.transform.position);
            var currentCell = Enemy.Grid.GetCell(gridPosition);
            _targetTower = currentCell?.OccupyingTower;
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            
            if (_targetTower) _targetTower.TakeDamage(Enemy.Damage);
        }

        public override void ExitState()
        {
            // Animation logic here.
        }
    }
}