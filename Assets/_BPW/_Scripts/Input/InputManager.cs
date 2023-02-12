using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.Collections.Specialized.BitVector32;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    private static List<InputAction> s_InputActions;

    [SerializeField] private InputActionAsset[] m_InputsAssets;

    private void Awake()
    {
        s_InputActions = new();

        foreach (var inputAsset in m_InputsAssets) {
            foreach (var actionMap in inputAsset.actionMaps.ToArray()) {
                foreach (var binding in actionMap.actions) {
                    s_InputActions.Add(binding);
                }
            }
            
            inputAsset.Enable();
        }
    }


    public static void Subscribe(Action<InputAction.CallbackContext> inputActionEvent, params Action<InputAction.CallbackContext>[] inputActionEvents)
    {
        inputActionEvents.Append(inputActionEvent);

        if (inputActionEvents.Length == 0)
            throw new ArgumentNullException("actions", new Exception("Input action parameters are empty, failed to subscribe to input action"));

        foreach (var action in inputActionEvents) {
            if (action != null) {

                InputAction inputAction = GetInputAction(action.Method.Name);
                if (inputAction != null) {
                    inputAction.performed += action;
                    continue;
                }

                Debug.LogError("Could not subscribe to input action with action: " + action.Method.Name);
            }
        }
    }


    public static void Unsubscribe(Action<InputAction.CallbackContext> inputActionEvent, params Action<InputAction.CallbackContext>[] inputActionEvents)
    {
        inputActionEvents.Append(inputActionEvent);

        if (inputActionEvents.Length == 0)
            throw new ArgumentNullException("actions", new Exception("Input action parameters are empty, failed to unsubscribe to input action"));

        foreach (var action in inputActionEvents) {
            if (action != null) {

                InputAction inputAction = GetInputAction(action.Method.Name);
                if (inputAction != null) {
                    inputAction.performed -= action;
                    continue;
                }

                Debug.LogError("Could not unsubscribe to input action with action: " + action.Method.Name);
            }
        }
    }

    private static InputAction GetInputAction(string actionName)
    {
        var inputAction = s_InputActions.Find(act => {
            var name = MakeTypeName(act.name);
            return "On" + name == actionName;
        });

        if (inputAction != null) {
            return inputAction;
        }

        Debug.LogError("No InputAction found to subscribe to");
        return null;
    }

    #region Create Typename methods
    /// <summary>
    /// Copied from Unity's <see cref="CSharpCodeHelpers"/> class
    /// </summary>
    private static string MakeIdentifier(string name, string suffix = "")
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (char.IsDigit(name[0]))
            name = "_" + name;

        // See if we have invalid characters in the name.
        var nameHasInvalidCharacters = false;
        for (var i = 0; i < name.Length; ++i) {
            var ch = name[i];
            if (!char.IsLetterOrDigit(ch) && ch != '_') {
                nameHasInvalidCharacters = true;
                break;
            }
        }

        // If so, create a new string where we remove them.
        if (nameHasInvalidCharacters) {
            var buffer = new StringBuilder();
            for (var i = 0; i < name.Length; ++i) {
                var ch = name[i];
                if (char.IsLetterOrDigit(ch) || ch == '_')
                    buffer.Append(ch);
            }

            name = buffer.ToString();
        }

        return name + suffix;
    }

    /// <summary>
    /// Copied from Unity's <see cref="CSharpCodeHelpers"/> class
    /// </summary>
    private static string MakeTypeName(string name, string suffix = "")
    {
        var symbolName = MakeIdentifier(name, suffix);
        if (char.IsLower(symbolName[0]))
            symbolName = char.ToUpper(symbolName[0]) + symbolName.Substring(1);
        return symbolName;
    }
    #endregion
}
