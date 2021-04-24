// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/MainControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainControl"",
    ""maps"": [
        {
            ""name"": ""Model"",
            ""id"": ""b5a85fdb-734f-46eb-a63d-a33ad37c1ffb"",
            ""actions"": [
                {
                    ""name"": ""DragDelta"",
                    ""type"": ""Value"",
                    ""id"": ""7d0ee3d0-e43b-4c11-8aa6-4832d9d7f20e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""be986660-d6d7-42e5-b526-56546bfdfffd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Handle"",
                    ""type"": ""Value"",
                    ""id"": ""f3641a26-44d8-477a-b6c8-3c31e6c2ca07"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""75395085-18d6-4740-b106-b40471929a48"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""DragDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1599701e-9b82-4287-89d6-88a014e044fe"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touchscreen"",
                    ""action"": ""DragDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e28fa37b-af19-4063-88a7-0dec52839766"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""335ef720-6929-4a27-acdb-593ed0dacfa9"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touchscreen"",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c282d6e0-40ea-4ff2-ab74-ecfc368c013f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & mouse"",
                    ""action"": ""Handle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44973e22-6c6a-4d8e-b6b2-6991210dc264"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touchscreen"",
                    ""action"": ""Handle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & mouse"",
            ""bindingGroup"": ""Keyboard & mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touchscreen"",
            ""bindingGroup"": ""Touchscreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Model
        m_Model = asset.FindActionMap("Model", throwIfNotFound: true);
        m_Model_DragDelta = m_Model.FindAction("DragDelta", throwIfNotFound: true);
        m_Model_Drag = m_Model.FindAction("Drag", throwIfNotFound: true);
        m_Model_Handle = m_Model.FindAction("Handle", throwIfNotFound: true);
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

    // Model
    private readonly InputActionMap m_Model;
    private IModelActions m_ModelActionsCallbackInterface;
    private readonly InputAction m_Model_DragDelta;
    private readonly InputAction m_Model_Drag;
    private readonly InputAction m_Model_Handle;
    public struct ModelActions
    {
        private @MainControl m_Wrapper;
        public ModelActions(@MainControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @DragDelta => m_Wrapper.m_Model_DragDelta;
        public InputAction @Drag => m_Wrapper.m_Model_Drag;
        public InputAction @Handle => m_Wrapper.m_Model_Handle;
        public InputActionMap Get() { return m_Wrapper.m_Model; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ModelActions set) { return set.Get(); }
        public void SetCallbacks(IModelActions instance)
        {
            if (m_Wrapper.m_ModelActionsCallbackInterface != null)
            {
                @DragDelta.started -= m_Wrapper.m_ModelActionsCallbackInterface.OnDragDelta;
                @DragDelta.performed -= m_Wrapper.m_ModelActionsCallbackInterface.OnDragDelta;
                @DragDelta.canceled -= m_Wrapper.m_ModelActionsCallbackInterface.OnDragDelta;
                @Drag.started -= m_Wrapper.m_ModelActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_ModelActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_ModelActionsCallbackInterface.OnDrag;
                @Handle.started -= m_Wrapper.m_ModelActionsCallbackInterface.OnHandle;
                @Handle.performed -= m_Wrapper.m_ModelActionsCallbackInterface.OnHandle;
                @Handle.canceled -= m_Wrapper.m_ModelActionsCallbackInterface.OnHandle;
            }
            m_Wrapper.m_ModelActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DragDelta.started += instance.OnDragDelta;
                @DragDelta.performed += instance.OnDragDelta;
                @DragDelta.canceled += instance.OnDragDelta;
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
                @Handle.started += instance.OnHandle;
                @Handle.performed += instance.OnHandle;
                @Handle.canceled += instance.OnHandle;
            }
        }
    }
    public ModelActions @Model => new ModelActions(this);
    private int m_KeyboardmouseSchemeIndex = -1;
    public InputControlScheme KeyboardmouseScheme
    {
        get
        {
            if (m_KeyboardmouseSchemeIndex == -1) m_KeyboardmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & mouse");
            return asset.controlSchemes[m_KeyboardmouseSchemeIndex];
        }
    }
    private int m_TouchscreenSchemeIndex = -1;
    public InputControlScheme TouchscreenScheme
    {
        get
        {
            if (m_TouchscreenSchemeIndex == -1) m_TouchscreenSchemeIndex = asset.FindControlSchemeIndex("Touchscreen");
            return asset.controlSchemes[m_TouchscreenSchemeIndex];
        }
    }
    public interface IModelActions
    {
        void OnDragDelta(InputAction.CallbackContext context);
        void OnDrag(InputAction.CallbackContext context);
        void OnHandle(InputAction.CallbackContext context);
    }
}
