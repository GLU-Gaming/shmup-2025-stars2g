using TMPro;
using UnityEditor;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{

    /*
     NOG TOE TE VOEGEN:
    POWERUPS DIE WORDEN OPGESLAGEN OM AAN DE SCORE TOE TE VOEGEN MAAR IS NU NOG NIET NODIG
     */
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
