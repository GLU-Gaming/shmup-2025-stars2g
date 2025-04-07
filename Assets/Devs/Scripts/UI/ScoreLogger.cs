using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreLogger : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text Rank;

    ScoreSystem scoreSystem;
    DataHandler dataHandler;

    private void Start()
    {
        scoreSystem = FindFirstObjectByType<ScoreSystem>();
        dataHandler = FindFirstObjectByType<DataHandler>();

        scoreText.text = "Score: " + scoreSystem.Score.ToString();
        if(dataHandler.CheckIfNewHigh(scoreSystem.Score))
        {
            Rank.text = "New High Score!";
        }
        else
        {
            Rank.text = "High Score:" + dataHandler.LoadHighScores(SceneManager.GetActiveScene().name);
        }
    }
}