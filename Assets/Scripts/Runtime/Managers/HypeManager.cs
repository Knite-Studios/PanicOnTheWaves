using System.Collections.Generic;
using Common;
using Common.Attributes;
using MiniGame;
using UnityEngine;

namespace Managers
{
    [InitializeSingleton]
    public class HypeManager : MonoSingleton<HypeManager>
    {
        public float perfectHypeTime = 2.0f;
        public float niceHypeTime = 5.0f;
        public float prematureHypeTime = 10.0f;

        private Queue<Hype> _hypes = new();
        private HypeTrigger _hypeTrigger;
        private bool _canSpawnHype = true;

        public void SpawnHype()
        {
            if (!_canSpawnHype) return;

            var hypeTrigger = PrefabManager.Create<HypeTrigger>(PrefabType.HypeTrigger);
            _hypeTrigger = hypeTrigger;
            hypeTrigger.OnHypeDestroyed += () =>
            {
                _hypes.TryPeek(out var curHype);
                if (curHype && curHype.Target == _hypeTrigger.transform) _hypes.Dequeue();
                _hypeTrigger = null;
                _canSpawnHype = true;
            };

            var hype = PrefabManager.Create<Hype>(PrefabType.Hype);
            hype.Initialize(hypeTrigger);
            _hypes.Enqueue(hype);
            
            _canSpawnHype = false;
        }

        public void TriggerHype()
        {
            if (_hypes.Count == 0 || !_hypeTrigger) return;
            
            var hype = _hypes.Dequeue();
            if (!hype)
            {
                // Check the next hype if the target is = to the hype trigger.
                _hypes.TryPeek(out var nextHype);
                if (nextHype && nextHype.Target == _hypeTrigger.transform)
                    hype = _hypes.Dequeue();
            }
            
            var distance = Vector3.Distance(hype.transform.position, _hypeTrigger.transform.position);
            Debug.Log($"Distance: {distance}");
            
            // TODO: Improve this lol.
            if (distance <= perfectHypeTime)
            {
                Debug.Log("PERFECT! 2x multiplier!");
            }
            else if (distance <= niceHypeTime)
            {
                Debug.Log("NICE CATCH! 1.5x multiplier!");
            }
            else if (distance <= prematureHypeTime)
            {
                Debug.Log("PREMATURE! 1.0x multiplier!");
            }
            else
            {
                Debug.Log("Missed Hype!");
            }
            
            Destroy(hype.gameObject);
            Destroy(_hypeTrigger.gameObject);
        }
    }
}