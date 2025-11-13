using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float lookSpeed = 100f;

    [SerializeField]
    Vector2 mouseSensitivity = new Vector2(1, 1);

    Camera myCamera;

    void Start()
    {
        myCamera = GetComponentInChildren<Camera>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

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
            myCamera.transform.Rotate(
                lookInput.y * Time.deltaTime * lookSpeed * mouseSensitivity.y,
                0, 0);
        }
    }
}
