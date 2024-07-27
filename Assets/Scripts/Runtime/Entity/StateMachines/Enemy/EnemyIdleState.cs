namespace Entity.StateMachines.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(string name, BaseEntity owner) : base(name, owner)
        {
        }

        public override void EnterState()
        {
            // Animation logic here.
            // Enemy.Anim.SetBool();
        }

        public override void ExitState()
        {
            // Animation logic here.
            // Enemy.Anim.SetBool();
        }
    }
}