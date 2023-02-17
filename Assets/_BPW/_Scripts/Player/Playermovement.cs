using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    private float m_MovementSpeed = 5.0f;

    private void OnEnable()
    {
        InputManager.Subscribe(OnMovement, OnFireLeft);
    }

    private void OnDisable()
    {
        InputManager.Unsubscribe(OnMovement, OnFireLeft);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        print("Movement: " + context.ReadValue<Vector2>());
    }

    public void OnFireLeft(InputAction.CallbackContext context)
    {
        var mousePos = InputManager.ReadActionValue<Vector2>("OnMousePosition");

        if (mousePos != Vector2.zero)
        {
            var mousePosWorldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            var hit = Physics2D.Raycast(mousePosWorldPoint, Vector3.forward);
            if (hit.collider)
            {
                var isWalkable = hit.transform.GetComponent<IWalkable>() != null;
                if (isWalkable)
                {
                    StartCoroutine(MoveTowards(hit.transform.position, 1));
                }
                return;
            }
        }
    }

    private IEnumerator MoveTowards(Vector3 target, float duration)
    {
        var elapsedTime = Time.deltaTime;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, target, elapsedTime / duration);
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
    }
}
public interface IWalkable
{

}
