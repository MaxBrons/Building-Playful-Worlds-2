using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Playermovement : MonoBehaviour
{
    private float m_MovementSpeed = 5.0f;

    public void OnMovement(CallbackContext context)
    {
        print("Movement: " + context.ReadValue<Vector2>());
    }

    public void OnFireLeft(CallbackContext context)
    {
        print("Mouse: " + context.ReadValue<float>());
    }
}
