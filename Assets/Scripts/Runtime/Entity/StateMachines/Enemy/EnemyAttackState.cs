namespace Entity.StateMachines.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        public EnemyAttackState(string name, BaseEntity owner) : base(name, owner)
        {
        }
        
        public override void EnterState()
        {
            // Animation logic here.
        }

        public override void ExitState()
        {
            // Animation logic here.
        }
    }
}