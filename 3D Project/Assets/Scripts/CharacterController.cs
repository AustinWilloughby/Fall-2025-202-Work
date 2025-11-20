using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float yOffset = 1;

    [SerializeField]
    float lookSpeed = 100f;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float jumpForce = 100f;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Vector2 mouseSensitivity = new Vector2(1, 1);

    Camera myCamera;

    Vector2 moveInput;
    Rigidbody rb;
    bool grounded;

    void Start()
    {
        grounded = true;
        myCamera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos += transform.forward * moveInput.y * moveSpeed * Time.fixedDeltaTime;
        pos += transform.right * moveInput.x * moveSpeed * Time.fixedDeltaTime;

        if (grounded)
        {
            rb.linearVelocity = Vector3.zero;

            RaycastHit hit;
            if (Physics.Raycast(pos, -transform.up, out hit, Mathf.Infinity, groundLayer))
            {
                pos = hit.point;
                pos.y += yOffset;
            }
        }

        rb.MovePosition(pos);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && grounded)
        {
            grounded = false;
            rb.AddForce(Vector3.up * jumpForce); 
        }
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 lookInput = ctx.ReadValue<Vector2>();

        if(lookInput.x != 0)
        {
            transform.Rotate(0f,
                lookInput.x * Time.deltaTime * lookSpeed * mouseSensitivity.x,
                0);
        }

        if (lookInput.y != 0)
        {
            float xAngle = myCamera.transform.rotation.eulerAngles.x;
            if(xAngle > 180)
            {
                xAngle -= 360;
            }

            xAngle += lookInput.y * Time.deltaTime * lookSpeed * mouseSensitivity.y;
            xAngle = Mathf.Clamp(xAngle, -89, 89);

            myCamera.transform.rotation = Quaternion.Euler(
                xAngle, 
                myCamera.transform.rotation.eulerAngles.y, 
                myCamera.transform.rotation.eulerAngles.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((groundLayer & (1 << collision.gameObject.layer)) != 0)
        {
            grounded = true;
        }
    }
}
