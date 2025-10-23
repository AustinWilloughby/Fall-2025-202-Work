using UnityEngine;

public class testScoreSingleton : MonoBehaviour
{
    private int score;
    private TextMesh text;

    public static testScoreSingleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        score = 0;

        text = GetComponent<TextMesh>();
        text.text = "Score: " + score.ToString();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void UpdateScore(int update)
    {
        score += update;

        if (text != null)
        {
            text.text = "Score: " + score.ToString();
        }
    }
}
