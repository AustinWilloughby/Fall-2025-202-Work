using UnityEngine;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    float lerpSpeed = 1.0f;

    [SerializeField]
    List<GameObject> pathPoints;

    int startingIndex = 0;
    int targetIndex = 1;
    float percent = 0;

    [SerializeField]
    TextMesh scoreText;

    int score = 0;

    // Update is called once per frame
    void Update()
    {
        percent += Time.deltaTime * lerpSpeed;

        transform.position = Vector2.Lerp(
            pathPoints[startingIndex].transform.position,
            pathPoints[targetIndex].transform.position,
            percent);

        if(percent >= 1.0f)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();

            percent = 0;
            startingIndex = targetIndex;
            targetIndex = (targetIndex + 1) % pathPoints.Count;
        }
    }
}
