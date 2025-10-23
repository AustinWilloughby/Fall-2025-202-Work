using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    TextMesh text;

    public static ScoreManager Instance { get; private set; }

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        text = GetComponent<TextMesh>();
        text.text = "Score: " + score.ToString();
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        text.text = "Score: " + score.ToString();
    }
}
