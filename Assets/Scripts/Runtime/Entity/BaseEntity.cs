using System.Collections.Generic;
using Interfaces;
using NaughtyAttributes;
using Systems.Attributes;
using UnityEngine;
using World;

namespace Entity
{
    public abstract class BaseEntity : MonoBehaviour, IAttributable, IDamageable
    {
        protected internal GridBehaviour Grid;
        protected internal Rigidbody Rb;
        protected internal Animator Anim;

        public Dictionary<GameAttribute, object> Attributes { get; } = new();
        [field: SerializeField, ReadOnly]
        public int CurrentHealth { get; protected set; }

        #region Attribute Getters

        public int MaxHealth => this.GetAttributeValue<int>(GameAttribute.MaxHealth);
        public int Damage => this.GetAttributeValue<int>(GameAttribute.Damage);

        #endregion

        protected virtual void Awake()
        {
            Grid = FindObjectOfType<GridBehaviour>();
            Rb = GetComponent<Rigidbody>();
            Anim = GetComponent<Animator>();

            ApplyBaseAttributes();
        }
        
        protected virtual void ApplyBaseAttributes()
        {
            Debug.Log("Applying base attributes.");
        }

        public virtual void TakeDamage(int damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
                Destroy(gameObject);
        }
    }
}