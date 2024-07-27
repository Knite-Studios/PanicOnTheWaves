using Entity;
using Interfaces;
using Managers;
using NaughtyAttributes;
using UnityEngine;

namespace World.Projectiles
{
    public class BaseProjectile : MonoBehaviour, IPoolObject
    {
        [SerializeField] protected float speed = 10.0f;
        [SerializeField] protected float lifetime = 5.0f;
        [SerializeField, Required] protected PrefabType prefabType;

        private int _damage;
        private BaseEntity _owner;
        private float _timer;

        private void Update()
        {
            transform.position += transform.right * (speed * Time.deltaTime);

            _timer -= Time.deltaTime;
            if (_timer <= 0) Remove();
        }

        private void OnEnable()
            => Reset();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out BaseEntity entity)) return;
            
            // Check if the projectile should do damage.
            if (entity == _owner) return;
            if (entity.GetType() == _owner.GetType()) return;

            // Apply damage.
            if (entity is IDamageable damageable)
            {
                Debug.Log($"Projectile hit {entity.name}.");
                damageable.TakeDamage(_damage);
                Remove();
            }
        }

        public void SetAttributes(int damage, BaseEntity owner)
        {
            _damage = damage;
            _owner = owner;
        }

        public void Reset()
            => _timer = lifetime;

        public void Remove()
            => PrefabManager.Destroy(prefabType, gameObject);
    }
}