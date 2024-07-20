using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame
{
    public class HypeTrigger : MonoBehaviour
    {
        public Action OnHypeTriggerDestroyed;
        
        private void Start()
        {
            var parentRect = transform.parent.GetComponent<RectTransform>();
            var height = GetComponent<RectTransform>().rect.height;

            var minHeight = parentRect.rect.min.y + height;
            var maxHeight = parentRect.rect.max.y - height;
            
            // Set the position of the hype trigger random between the min and max height.
            var position = transform.localPosition;
            position.y = Random.Range(minHeight, maxHeight);
            transform.localPosition = position;
        }

        private void Update()
        {
            if (InputManager.Combo.triggered)
            {
                HypeManager.Instance.TriggerHype();
            }
        }
        
        private void OnDestroy()
        {
            // Invoke the event first.
            OnHypeTriggerDestroyed?.Invoke();
            
            // Unsubscribe to all handlers.
            foreach (var d in OnHypeTriggerDestroyed!.GetInvocationList())
            {
                OnHypeTriggerDestroyed -= (Action) d;
            }
        }
    }
}