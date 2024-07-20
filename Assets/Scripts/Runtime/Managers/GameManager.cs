using System;
using System.Linq;
using Common;
using Common.Attributes;
using Common.Utils;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();

            InitializeManagers();
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

        private void Update()
        {
            // TODO: Temporary only for prototype.
            if (InputManager.HypeTest.triggered)
            {
                HypeManager.Instance.SpawnHype();
            }
        }
    }
}