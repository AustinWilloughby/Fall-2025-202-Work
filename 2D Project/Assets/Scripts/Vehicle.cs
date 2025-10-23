using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 5f;

    [SerializeField]
    Vector2 movementDirection;

    [SerializeField]
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    private void FixedUpdate()
    {
        if (movementDirection.magnitude > 0)
        {
            Vector2 velocity = movementDirection * movementSpeed * Time.fixedDeltaTime;
            Vector2 newPos = (Vector2)transform.position + velocity;
            rb.MovePosition(newPos);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
        if(movementDirection.y < 0)
        {
            ScoreManager.Instance.UpdateScore(1);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector2.zero, movementDirection * 5);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rb.linearVelocity);
    }
}
