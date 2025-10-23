using UnityEngine;

public class DeltaTimeDemo : MonoBehaviour
{
    [SerializeField, Range(1, 200)]
    int targetFrameRate = 100;

    [SerializeField]
    GameObject frameObject;

    [SerializeField]
    GameObject deltaObject;

    [SerializeField]
    GameObject jumpObject;

    [SerializeField]
    float jumpTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer += Time.deltaTime;

        if(jumpTimer >= 5)
        {
            jumpTimer = 0;

            Vector2 jumpPos = jumpObject.transform.position;
            jumpPos.x += 5;
            jumpObject.transform.position = jumpPos;
        }




        Application.targetFrameRate = targetFrameRate;

        Vector2 pos = frameObject.transform.position;
        pos.x += 0.01f; //Hopefully 1 unit a second, provided a framerate of 100fps
        frameObject.transform.position = pos;

        pos = deltaObject.transform.position;
        pos.x += 1.0f * Time.deltaTime; //Will always move at 1 unit per second
        deltaObject.transform.position = pos;
    }
}
