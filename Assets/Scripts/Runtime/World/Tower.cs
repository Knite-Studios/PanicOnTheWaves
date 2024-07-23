using UnityEngine;
using UnityEngine.Events;

namespace World
{
    public class Tower : MonoBehaviour
    {
        public event UnityAction OnTowerDestroyed;
        
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