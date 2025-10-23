using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    TextMesh textMesh;

    public static ScoreManager Instance { get; private set; }

    private void Awake()
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
        textMesh = GetComponent<TextMesh>();
        textMesh.text = "Score: " + score.ToString();
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        textMesh.text = "Score: " + score.ToString();
    }
}
