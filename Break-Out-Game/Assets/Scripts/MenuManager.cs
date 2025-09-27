using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TMP_InputField nameInput;

    private void Start()
    {
        if (StorageManager.Instance != null)
        {
            ShowBestScore();
        }
    }

    private void ShowBestScore()
    {
        if (bestScoreText != null)
        {
            BestScore bestScore = StorageManager.Instance.bestScore;
            if (bestScore != null)
            {
                Debug.Log($"Best Score: {bestScore.bestScore}, Player: {bestScore.bestPlayerName}");
                bestScoreText.text = "Best Score: " + bestScore.bestPlayerName + " " + bestScore.bestScore;
            }
        }
    }

    public void StartGame()
    {
        if (StorageManager.Instance != null)
        {
            StorageManager.Instance.playerName = nameInput.text;
            Debug.Log($"Player name: {nameInput.text}");
        }

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        if (StorageManager.Instance != null)
        {
            StorageManager.Instance.StoreBestScore();

        }

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
