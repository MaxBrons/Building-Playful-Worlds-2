using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    private float m_MovementSpeed = 5.0f;

    private void OnEnable()
    {
        InputManager.Subscribe(OnMovement);
    }

    private void OnDisable()
    {
        InputManager.Unsubscribe(OnMovement);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        print("Movement: " + context.ReadValue<Vector2>());
    }
}
