using Common;
using Common.Attributes;
using UnityEngine.InputSystem;

namespace Managers
{
    [InitializeSingleton]
    public class InputManager : MonoSingleton<InputManager>
    {
        private Inputs _inputs;
        
        public static InputAction Combo => Instance._inputs.Controls.Combo;
        public static InputAction HypeTest => Instance._inputs.Controls.HypeTest;

        protected override void Awake()
        {
            base.Awake();
            _inputs = new Inputs();
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
    }
}