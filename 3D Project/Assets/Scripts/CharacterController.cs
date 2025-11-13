using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float lookSpeed = 100f;
    [SerializeField] Vector2 lookSensitivity = new Vector2(1, 1);

    Camera myCamera;

    void Start()
    {
        myCamera = GetComponentInChildren<Camera>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        //Vector2 lookInput = ctx.ReadValue<Vector2>();
        //
        //if(lookInput.x != 0f)
        //{
        //    transform.Rotate(0,
        //        lookSpeed * Time.deltaTime * lookInput.x * lookSensitivity.x,
        //        0);
        //}
        //
        //if(lookInput.y != 0f)
        //{
        //    myCamera.transform.Rotate(
        //        lookSpeed * Time.deltaTime * lookInput.y * lookSensitivity.y,
        //        0, 0);
        //}
    }
}
