using System.Collections.Generic;
using Interfaces;
using Systems.Attributes;
using UnityEngine;

namespace Entity
{
    public abstract class BaseEntity : MonoBehaviour, IAttributable, IDamageable
    {
        public Dictionary<GameAttribute, object> Attributes { get; } = new();
        public int CurrentHealth { get; protected set; }

        #region Attribute Getters

        public int MaxHealth => this.GetAttributeValue<int>(GameAttribute.MaxHealth);
        public int Damage => this.GetAttributeValue<int>(GameAttribute.Damage);

        #endregion
        
        protected internal Rigidbody Rb;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();

            ApplyBaseAttributes();
        }
        
        protected virtual void ApplyBaseAttributes()
        {
            Debug.Log("Applying base attributes.");
        }

        public virtual void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }
    }
}