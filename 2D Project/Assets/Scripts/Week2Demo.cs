using UnityEngine;

public class Week2Demo : MonoBehaviour
{
    [SerializeField]
    int favoriteNumber = 14;

    [SerializeField]
    GameObject ballPrefab;

    // Start is called once before the first execution
    // of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        obj.GetComponent<SpriteRenderer>().color = new Color(
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f));
    }
}
