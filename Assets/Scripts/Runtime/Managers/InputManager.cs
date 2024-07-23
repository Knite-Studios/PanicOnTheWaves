using Common;
using Common.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Managers
{
    [InitializeSingleton]
    public class InputManager : MonoSingleton<InputManager>
    {
        public static Vector2 MousePosition => Mouse.current.position.ReadValue();
        
        #region Gameplay Hooks
        
        public UnityAction StartSelectGrid, StopSelectGrid;
        
        #endregion

        #region Player Actions

        public static InputAction Combo => Instance._inputs.Controls.Combo;
        public static InputAction HypeTest => Instance._inputs.Controls.HypeTest;
        
        public static InputAction MouseDelta => Instance._inputs.Controls.MouseDelta;
        public static InputAction Select => Instance._inputs.Controls.Select;
        
        public static InputAction Menu => Instance._inputs.Controls.Menu;

        #endregion
        
        public Inputs.ControlsActions Controls => _inputs.Controls;
        
        private Inputs _inputs;

        protected override void Awake()
        {
            base.Awake();

            _inputs = new Inputs();
            InteractionStack.Register();
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

        public static class InteractionStack
        {
            public static void Register()
            {
                var input = Instance;
                input.Controls.Select.started += Select_OnStarted;
                input.Controls.Select.canceled += Select_OnCanceled;
                input.Controls.Select.performed += Select_OnPerformed;
            }

            #region Controls.Select

            private static void Select_OnStarted(InputAction.CallbackContext context)
            {
                Instance.StartSelectGrid?.Invoke();
            }
            
            private static void Select_OnCanceled(InputAction.CallbackContext context)
            {
                Instance.StopSelectGrid?.Invoke();
            }
            
            private static void Select_OnPerformed(InputAction.CallbackContext context)
            {
                // Do something when the select action is performed.
            }

            #endregion
        }
    }
}