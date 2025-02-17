using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    TextMeshProUGUI scoreTextUI;

    void Start()
    {
        scoreTextUI = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int score)
    {
        if (scoreTextUI != null)
        {
            scoreTextUI.text = "Score: " + score.ToString("0000000");
        }
    }
}