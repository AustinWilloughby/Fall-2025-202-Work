using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField]
    LayerMask interactableLayers;

    Camera thisCamera;

    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if(!ctx.performed)
        {
            return;
        }

        Ray raycast = thisCamera.ScreenPointToRay(Mouse.current.position.value);

        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit, Mathf.Infinity, interactableLayers))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
