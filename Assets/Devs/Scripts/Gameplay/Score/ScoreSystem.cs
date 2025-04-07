using TMPro;
using UnityEditor;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public int Score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] string scorePrefix = "Score";

    public void AddScore(int ScoreToAdd)
    {
        Score += ScoreToAdd;
    }

    private void Update()
    {
        scoreText.text = scorePrefix + ": " + Score;
    }
}
