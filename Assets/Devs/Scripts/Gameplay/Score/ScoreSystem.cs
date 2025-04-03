using TMPro;
using UnityEditor;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public int Score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] string scorePrefix = "Score";

    int S_score;
    int A_score;
    int B_score;
    int C_score;
    int D_score;


    public void AddScore(int ScoreToAdd)
    {
        Score += ScoreToAdd;
    }

    private void Update()
    {
        scoreText.text = scorePrefix + ": " + Score;
    }
}
