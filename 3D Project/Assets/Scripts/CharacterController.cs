using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] float lookSpeed = 100f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector2 lookSensitivity = new Vector2(1, 1);
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundingOffset = 1.0f;

    Vector2 moveInput;

    Camera myCamera;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;

        Vector3 pos = transform.position;

        pos += transform.forward * moveInput.y * moveSpeed * Time.fixedDeltaTime;
        pos += transform.right * moveInput.x * moveSpeed * Time.fixedDeltaTime;

        RaycastHit hit;
        if(Physics.Raycast(pos, -transform.up, out hit, Mathf.Infinity, groundMask))
        {
            pos.y = hit.point.y + groundingOffset;
        }

        rb.MovePosition(pos);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 lookInput = ctx.ReadValue<Vector2>();
        
        if(lookInput.x != 0f)
        {
            transform.Rotate(0,
                lookSpeed * Time.deltaTime * lookInput.x * lookSensitivity.x,
                0);
        }
        
        if(lookInput.y != 0f)
        {
            myCamera.transform.Rotate(
                lookSpeed * Time.deltaTime * lookInput.y * lookSensitivity.y,
                0, 0);
        }
    }
}
