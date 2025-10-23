using UnityEngine;

public class Score : MonoBehaviour
{
    private int score = -1;

    private TextMesh text;

    private void Start()
    {
        text = GetComponent<TextMesh>();
    }

    public void UpdateScore(int increase)
    {
        score += increase;
        text.text = "Score: " + score.ToString();
    }
}
