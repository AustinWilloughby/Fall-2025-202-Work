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
    GameObject jumperObject;

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

            Vector3 newPos = jumperObject.transform.position;
            newPos.x += 4;
            jumperObject.transform.position = newPos;
        }






        Application.targetFrameRate = targetFrameRate;

        Vector3 pos = frameObject.transform.position;
        pos.x += 0.01f; //Hopefully 1 unit per second, assuming 100fps
        frameObject.transform.position = pos;

        pos = deltaObject.transform.position;
        pos.x += 1.0f * Time.deltaTime; 
        deltaObject.transform.position = pos;
    }
}
