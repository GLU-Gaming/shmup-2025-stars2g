using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataHandler : MonoBehaviour
{
    [System.Serializable]
    public class HighScoreData
    {
        public Dictionary<string, List<int>> highScores = new Dictionary<string, List<int>>();
    }

    private string highScoreFilePath;

    private void Awake()
    {
        highScoreFilePath = Path.Combine(Application.persistentDataPath, "highscores.json");
    }

    public void SaveHighScores(string sceneName, List<int> highScores)
    {
        HighScoreData data = LoadHighScoreData();
        data.highScores[sceneName] = highScores;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(highScoreFilePath, json);
    }

    public List<int> LoadHighScores(string sceneName)
    {
        HighScoreData data = LoadHighScoreData();
        if (data.highScores.ContainsKey(sceneName))
        {
            return data.highScores[sceneName];
        }
        else
        {
            return new List<int>();
        }
    }

    private HighScoreData LoadHighScoreData()
    {
        if (File.Exists(highScoreFilePath))
        {
            string json = File.ReadAllText(highScoreFilePath);
            return JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            return new HighScoreData();
        }
    }

    public void AddHighScore(int score)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        List<int> highScores = LoadHighScores(sceneName);
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a)); // Sort in descending order
        SaveHighScores(sceneName, highScores);
    }

    public bool CheckIfNewHigh(int score)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        List<int> highScores = LoadHighScores(sceneName);
        if (highScores.Count == 0)
        {
            return true;
        }
        int highestScore = highScores[0];
        return score > highestScore;
    }

    public List<int> GetHighScores()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return LoadHighScores(sceneName);
    }
}
