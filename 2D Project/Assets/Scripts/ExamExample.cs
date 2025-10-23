using UnityEngine;
using UnityEngine.InputSystem;

public class ExamExample : MonoBehaviour
{
    Vector2 moveDirection;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDirection.magnitude > 0)
        {
            rb.AddForce(moveDirection * 500 * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
}
