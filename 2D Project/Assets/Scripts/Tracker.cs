using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tracker : MonoBehaviour
{
    [SerializeField, ReadOnly(true)]
    Vector3 mousePosition;

    bool isTracking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = transform.position.z;

        if (isTracking)
        {
            transform.position = mousePosition;
        }
        else
        {
            Vector2 targetPos = mousePosition - transform.position;
            float targetAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            targetAngle -= 90;
            Quaternion newRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = newRotation;
        }
    }


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isTracking = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isTracking = false;
        }
    }

}
