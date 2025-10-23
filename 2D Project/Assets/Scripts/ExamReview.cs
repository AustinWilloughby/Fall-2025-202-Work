using UnityEngine;
using UnityEngine.InputSystem;

public class ExamReview : MonoBehaviour
{

    Vector2 moveDirection;

    private void Awake()
    {
        moveDirection = Vector2.zero;

        GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 newPos = transform.position;
        //
        //if(Input.GetKey(KeyCode.W))
        //{
        //    newPos.y += Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    newPos.y -= Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    newPos.x -= Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    newPos.x += Time.deltaTime;
        //}
        //transform.position = newPos;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 newPos = transform.position;
        newPos += context.ReadValue<Vector2>();
        transform.position = newPos;
    }
}
