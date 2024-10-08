//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Resources/Inputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Inputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Controls"",
            ""id"": ""4c87c93a-17e9-44f7-a44c-6ebd1b8aedf5"",
            ""actions"": [
                {
                    ""name"": ""Combo"",
                    ""type"": ""Button"",
                    ""id"": ""a92e9663-92ef-41e0-a1cd-c76df8cb8245"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HypeTest"",
                    ""type"": ""Button"",
                    ""id"": ""14e3ac40-6832-4794-9c6b-30791108c218"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""54aeec92-de48-4eb1-a934-ae1ccfbc157f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""ac796689-873e-4e82-b7d2-08e3589edff9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""d15f2605-b30c-44f7-8d21-91550f9b7799"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""215dca6c-70ea-4521-b303-f2b126c46cf6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Combo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc3885d5-29f5-48fa-9f37-4e2de7f5e161"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HypeTest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1365a2e6-0b0e-4244-bb28-c215b0be64ac"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8da82de-8113-45d2-b9a5-cc642265628f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78e185fa-6bf1-44dc-908d-c645c19eb37d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controls
        m_Controls = asset.FindActionMap("Controls", throwIfNotFound: true);
        m_Controls_Combo = m_Controls.FindAction("Combo", throwIfNotFound: true);
        m_Controls_HypeTest = m_Controls.FindAction("HypeTest", throwIfNotFound: true);
        m_Controls_MouseDelta = m_Controls.FindAction("MouseDelta", throwIfNotFound: true);
        m_Controls_Select = m_Controls.FindAction("Select", throwIfNotFound: true);
        m_Controls_Menu = m_Controls.FindAction("Menu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Controls
    private readonly InputActionMap m_Controls;
    private List<IControlsActions> m_ControlsActionsCallbackInterfaces = new List<IControlsActions>();
    private readonly InputAction m_Controls_Combo;
    private readonly InputAction m_Controls_HypeTest;
    private readonly InputAction m_Controls_MouseDelta;
    private readonly InputAction m_Controls_Select;
    private readonly InputAction m_Controls_Menu;
    public struct ControlsActions
    {
        private @Inputs m_Wrapper;
        public ControlsActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Combo => m_Wrapper.m_Controls_Combo;
        public InputAction @HypeTest => m_Wrapper.m_Controls_HypeTest;
        public InputAction @MouseDelta => m_Wrapper.m_Controls_MouseDelta;
        public InputAction @Select => m_Wrapper.m_Controls_Select;
        public InputAction @Menu => m_Wrapper.m_Controls_Menu;
        public InputActionMap Get() { return m_Wrapper.m_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsActions set) { return set.Get(); }
        public void AddCallbacks(IControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_ControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ControlsActionsCallbackInterfaces.Add(instance);
            @Combo.started += instance.OnCombo;
            @Combo.performed += instance.OnCombo;
            @Combo.canceled += instance.OnCombo;
            @HypeTest.started += instance.OnHypeTest;
            @HypeTest.performed += instance.OnHypeTest;
            @HypeTest.canceled += instance.OnHypeTest;
            @MouseDelta.started += instance.OnMouseDelta;
            @MouseDelta.performed += instance.OnMouseDelta;
            @MouseDelta.canceled += instance.OnMouseDelta;
            @Select.started += instance.OnSelect;
            @Select.performed += instance.OnSelect;
            @Select.canceled += instance.OnSelect;
            @Menu.started += instance.OnMenu;
            @Menu.performed += instance.OnMenu;
            @Menu.canceled += instance.OnMenu;
        }

        private void UnregisterCallbacks(IControlsActions instance)
        {
            @Combo.started -= instance.OnCombo;
            @Combo.performed -= instance.OnCombo;
            @Combo.canceled -= instance.OnCombo;
            @HypeTest.started -= instance.OnHypeTest;
            @HypeTest.performed -= instance.OnHypeTest;
            @HypeTest.canceled -= instance.OnHypeTest;
            @MouseDelta.started -= instance.OnMouseDelta;
            @MouseDelta.performed -= instance.OnMouseDelta;
            @MouseDelta.canceled -= instance.OnMouseDelta;
            @Select.started -= instance.OnSelect;
            @Select.performed -= instance.OnSelect;
            @Select.canceled -= instance.OnSelect;
            @Menu.started -= instance.OnMenu;
            @Menu.performed -= instance.OnMenu;
            @Menu.canceled -= instance.OnMenu;
        }

        public void RemoveCallbacks(IControlsActions instance)
        {
            if (m_Wrapper.m_ControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_ControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ControlsActions @Controls => new ControlsActions(this);
    public interface IControlsActions
    {
        void OnCombo(InputAction.CallbackContext context);
        void OnHypeTest(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}
