using System;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StorageManager : MonoBehaviour
{
    public static StorageManager Instance;

    [ContextMenu("Clear Record")] 
    public void ClearRecordButton() => ClearRecord();

    public string playerName { get; set; }
    public BestScore bestScore { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        LoadBestScore();
    }

    public void UpdateBestScore(int score)
    {
        bestScore.bestPlayerName = playerName;
        bestScore.bestScore = score;
    }

    public void ClearRecord()
    {
        bestScore.bestPlayerName = "NO Player";
        bestScore.bestScore = 0;
        StoreBestScore();
    }

    public void StoreBestScore()
    {
        try
        {
            string json = JsonUtility.ToJson(bestScore, true);
            string path = Path.Combine(Application.persistentDataPath, "bestscore.json");
            File.WriteAllText(path, json);
            Debug.Log($"Score saved: {json}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save score: {e.Message}");
        }
    }

    public void LoadBestScore()
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "bestscore.json");

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                bestScore = JsonUtility.FromJson<BestScore>(json);
                Debug.Log($"Loaded - Best Score: {bestScore.bestScore}, Player: {bestScore.bestPlayerName}");
            }
            else
            {
                Debug.Log("No save file found, using defaults");
                bestScore = new BestScore("No Best Player", 0);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load score: {e.Message}");
            bestScore = new BestScore("No Player", 0);
        }
    }
}


[System.Serializable]
public class BestScore
{
    public string bestPlayerName;
    public int bestScore;

    public BestScore()
    {
        bestPlayerName = "";
        bestScore = 0;
    }

    public BestScore(string playerName, int score)
    {
        bestPlayerName = playerName;
        bestScore = score;
    }
}
