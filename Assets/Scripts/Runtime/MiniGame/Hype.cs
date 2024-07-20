using System;
using Extensions;
using UnityEngine;

namespace MiniGame
{
    public class Hype : MonoBehaviour
    {
        public Action OnHypeDestroyed;

        [SerializeField] private float speed = 200.0f;

        public Transform Target { get; private set; }

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            if (!(transform.position.y < Target.position.y - 20.0f)) return;
            
            Debug.Log("<color=red>Missed the hype without pressing!</color>");
            Destroy(gameObject);
            Destroy(Target!.gameObject);
        }

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (!other.TryGetComponent<HypeTrigger>(out var hypeTrigger)) return;
        //     if (hypeTrigger != _owner) return;
        //     Debug.Log("Missed Hype!");
        // }
        //
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (!other.TryGetComponent<HypeTrigger>(out var hypeTrigger)) return;
        //     if (hypeTrigger != _owner) return;
        //     
        //     Debug.Log("Missed Hypeeeeeeeeeeeeee!");
        // }
        
        private void OnDestroy()
        {
            // Invoke the event first.
            OnHypeDestroyed?.Invoke();
            
            // Unsubscribe to all handlers.
            foreach (var d in OnHypeDestroyed!.GetInvocationList())
            {
                OnHypeDestroyed -= (Action) d;
            }
        }

        public void Initialize(HypeTrigger owner)
        {
            Target = owner.transform;
            _rb.velocity = Vector2.down * speed;
        }
    }
}