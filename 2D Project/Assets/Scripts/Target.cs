using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    Vector2 screenBounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenBounds.y = Camera.main.orthographicSize;
        screenBounds.x = screenBounds.y * Camera.main.aspect;
        MoveToRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Camera.main.transform.position, screenBounds * 2);
    }

    private void MoveToRandomPoint()
    {
        Vector2 camPos = Camera.main.transform.position;

        transform.position = new Vector2(
            Random.Range(camPos.x - screenBounds.x, camPos.x + screenBounds.x),
            Random.Range(camPos.y - screenBounds.y, camPos.y + screenBounds.y)
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager.Instance.UpdateScore(1);
        MoveToRandomPoint();
    }
}
