using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput Input;

    private void Start()
    {
        Input = FindObjectOfType<PlayerInput>();
        foreach (var item in Input.actions.actionMaps)
        {
            item.Enable();
        }
    }
}
