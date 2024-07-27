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
            // Enemy.Anim.SetTrigger();
        }
        
        public override void UpdateState()
        {
            base.UpdateState();
            
            Enemy.Attack();
        }

        public override void ExitState()
        {
            // Animation logic here.
            // Enemy.Anim.ResetTrigger();
        }
    }
}