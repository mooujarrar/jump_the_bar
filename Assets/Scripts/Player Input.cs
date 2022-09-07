// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Input"",
    ""maps"": [
        {
            ""name"": ""Caracter control"",
            ""id"": ""a2e52a6b-442a-4442-9cbe-47437871e232"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e510265f-6e83-40b8-abf0-2939cf89e1b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Button"",
                    ""id"": ""f9dd8b2b-0003-4ba8-8bc4-9fc9a62b89a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f72dde8b-6f83-4b88-b661-1f577e8875f2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a9a9ea6d-424a-466a-898a-67bbecf701dc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fa7b6aa0-8419-4dc3-869a-cee5bf06b792"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""720c8c11-ffa7-4b05-9d07-088faa274512"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Caracter control
        m_Caractercontrol = asset.FindActionMap("Caracter control", throwIfNotFound: true);
        m_Caractercontrol_Jump = m_Caractercontrol.FindAction("Jump", throwIfNotFound: true);
        m_Caractercontrol_Turn = m_Caractercontrol.FindAction("Turn", throwIfNotFound: true);
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

    // Caracter control
    private readonly InputActionMap m_Caractercontrol;
    private ICaractercontrolActions m_CaractercontrolActionsCallbackInterface;
    private readonly InputAction m_Caractercontrol_Jump;
    private readonly InputAction m_Caractercontrol_Turn;
    public struct CaractercontrolActions
    {
        private @PlayerInput m_Wrapper;
        public CaractercontrolActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Caractercontrol_Jump;
        public InputAction @Turn => m_Wrapper.m_Caractercontrol_Turn;
        public InputActionMap Get() { return m_Wrapper.m_Caractercontrol; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CaractercontrolActions set) { return set.Get(); }
        public void SetCallbacks(ICaractercontrolActions instance)
        {
            if (m_Wrapper.m_CaractercontrolActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_CaractercontrolActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CaractercontrolActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CaractercontrolActionsCallbackInterface.OnJump;
                @Turn.started -= m_Wrapper.m_CaractercontrolActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_CaractercontrolActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_CaractercontrolActionsCallbackInterface.OnTurn;
            }
            m_Wrapper.m_CaractercontrolActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
            }
        }
    }
    public CaractercontrolActions @Caractercontrol => new CaractercontrolActions(this);
    public interface ICaractercontrolActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
    }
}
