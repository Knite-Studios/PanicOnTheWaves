using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Attributes;
using Common.Utils;
using NaughtyAttributes;
using Scriptable;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static UnityAction<List<EnemyInfo>> OnWaveStart;

        protected override void Awake()
        {
            base.Awake();

            InitializeManagers();
        }
        
        private void Update()
        {
            // TODO: Temporary only for prototype.
            if (InputManager.HypeTest.triggered)
            {
                HypeManager.Instance.SpawnHype();
                Debug.Log("Hype spawned!");
            }
        }

        /// <summary>
        /// Initialize managers with the InitializeSingleton attribute.
        /// </summary>
        private void InitializeManagers()
        {
            var singletons = Utils.GetTypesFor<InitializeSingleton>();
            foreach (var method in singletons.Select(
                         singleton => singleton.GetMethod("Initialize")))
            {
                method?.Invoke(null, null);
            }
        }
        
        [Button]
        public void StartWave()
        {
            var wave = WaveManager.Instance.GetNextWave();
            OnWaveStart?.Invoke(wave);
        }
    }
}