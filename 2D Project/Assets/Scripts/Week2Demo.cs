using UnityEngine;

public class Week2Demo : MonoBehaviour
{
    [SerializeField]
    int favoriteNumber = 14;

    [SerializeField]
    GameObject ballPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(favoriteNumber);

    }

    // Update is called once per frame
    void Update()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.GetComponent<SpriteRenderer>().color = new Color(
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f), 
            Random.Range(0.0f, 1.0f));
    }
}
