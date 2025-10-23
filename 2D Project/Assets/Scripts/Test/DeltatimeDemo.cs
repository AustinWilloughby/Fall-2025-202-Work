using UnityEngine;

public class DeltatimeDemo : MonoBehaviour
{
    [SerializeField, Range(30, 100)]
    int targetFrameRate = 60;

    [SerializeField]
    GameObject frameObject;

    [SerializeField]
    GameObject deltaObject;


    [SerializeField]
    GameObject frameSpeed;

    [SerializeField]
    GameObject deltaSpeed;

    private void Start()
    {
    }

    void Update()
    {
        Application.targetFrameRate = targetFrameRate;

        Vector2 pos = frameObject.transform.position;
        pos.x += 0.01f;
        frameObject.transform.position = pos;

        pos = deltaObject.transform.position;
        pos.x += 0.6f * Time.deltaTime;
        deltaObject.transform.position = pos;


        Vector2 scale = new Vector2(0.01f * targetFrameRate, 0.3f);
        frameSpeed.transform.localScale = scale;

        scale.x = 0.6f * 1.0f;
        deltaSpeed.transform.localScale = scale;
    }

}