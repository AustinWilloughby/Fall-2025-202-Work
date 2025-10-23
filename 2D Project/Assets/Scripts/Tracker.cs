using UnityEngine;
using UnityEngine.InputSystem;

public class Tracker : MonoBehaviour
{
    [SerializeField]
    Vector3 mousePosition;

    bool mouseDown = false;


    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        Vector3 mappedPos = Camera.main.ScreenToWorldPoint(mousePosition);
        mappedPos.z = transform.position.z;
        if (mouseDown)
        {
            mappedPos.y = transform.position.y;
            transform.position = mappedPos;
        }
        else
        {
            Vector3 targetPos = mappedPos - transform.position;
            float targetSpin = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            Quaternion turnRotation = Quaternion.Euler(0, 0, targetSpin - 90);
            transform.rotation = turnRotation;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            mouseDown = true;
        } 
        else if (context.phase == InputActionPhase.Canceled)
        {
            mouseDown = false;
        }
    }
}
