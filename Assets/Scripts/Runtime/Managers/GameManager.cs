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
            
            // Initialize other managers.
            var singletons = Utils.GetTypesFor<InitializeSingleton>();
            foreach (var method in singletons.Select(
                singleton => singleton.GetMethod("Initialize")))
            {
                method?.Invoke(null, null);
            }
        }
    }
}