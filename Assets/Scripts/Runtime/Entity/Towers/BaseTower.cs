using Common.Attributes;
using Scriptable.Scriptable;
using Systems.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Entity.Towers
{
    public class BaseTower : BaseEntity
    {
        [TitleHeader("Base Tower Settings")]
        [SerializeField] private TowerInfo info;
        
        public event UnityAction OnTowerDestroyed;

        protected override void ApplyBaseAttributes()
        {
            this.GetOrCreateAttribute(GameAttribute.MaxHealth, info.health);
            this.GetOrCreateAttribute(GameAttribute.Damage, info.damage);
            
            CurrentHealth = MaxHealth;
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
    }
}