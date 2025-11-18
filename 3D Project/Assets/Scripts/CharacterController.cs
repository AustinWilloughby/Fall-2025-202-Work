using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float yOffset = 1;

    [SerializeField]
    float lookSpeed = 100f;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Vector2 mouseSensitivity = new Vector2(1, 1);

    Camera myCamera;

    Vector2 moveInput;
    Rigidbody rb;

    void Start()
    {
        myCamera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector3.zero;
        Vector3 pos = transform.position;
        pos += transform.forward * moveInput.y * moveSpeed * Time.fixedDeltaTime;
        pos += transform.right * moveInput.x * moveSpeed * Time.fixedDeltaTime;

        RaycastHit hit;
        if(Physics.Raycast(pos, -transform.up, out hit, Mathf.Infinity, groundLayer))
        {
            pos = hit.point;
            pos.y += yOffset;
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

        if(lookInput.x != 0)
        {
            transform.Rotate(0f,
                lookInput.x * Time.deltaTime * lookSpeed * mouseSensitivity.x,
                0);
        }

        if (lookInput.y != 0)
        {
            if (transform.rotation.eulerAngles.x < 89
                || transform.rotation.eulerAngles.x > 271)
            {
                myCamera.transform.Rotate(
                    lookInput.y * Time.deltaTime * lookSpeed * mouseSensitivity.y,
                    0, 0);
            }
        }
    }
}
