using Systems.Attributes;
using UnityEngine;

namespace Entity.StateMachines.Enemy
{
    public class EnemyMoveState : EnemyBaseState
    {
        private int _currentWaypointIndex;

        public EnemyMoveState(string name, BaseEntity owner) : base(name, owner)
        {
        }

        public override void EnterState()
        {
            // Animation logic here.
            // Enemy.Anim.SetBool();
        }
        
        public override void FixedUpdateState()
        {
            MoveToPath();
        }

        public override void ExitState()
        {
            // Animation logic here.
            // Enemy.Anim.SetBool();
        }

        private void MoveToPath()
        {
            if (Enemy.PathWaypoints == null || Enemy.PathWaypoints.Count == 0) return;
            
            if (_currentWaypointIndex >= Enemy.PathWaypoints.Count)
            {
                // TODO: Implement enemy reached the end of the path.
                Enemy.Rb.velocity = Vector3.zero;
                return;
            }
            
            var targetPosition = Enemy.PathWaypoints[_currentWaypointIndex];
            var direction = (targetPosition - Enemy.transform.position).normalized;
            
            // Rotate towards the target direction.
            // TODO: Implement a better rotation system later probably.
            var targetRotation = Quaternion.Euler(0, 180, 0); 
            Enemy.transform.rotation = Quaternion.Slerp(Enemy.transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
            
            // Move towards the target position.
            Enemy.Rb.MovePosition(Enemy.transform.position + direction * (Enemy.Speed * Time.fixedDeltaTime));

            if (Vector3.Distance(Enemy.transform.position, targetPosition) < 0.1f)
                _currentWaypointIndex++;
        }
    }
}