using TMPro;
using UnityEngine;

public class ScoreLogger : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text Rank;

    ScoreSystem scoreSystem;

    private void Start()
    {
        ScoreSystem.FindFirstObjectByType<ScoreSystem>();

    }
}