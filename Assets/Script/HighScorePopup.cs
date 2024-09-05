using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScorePopup : MonoBehaviour
{
    public TextMeshProUGUI ScoreLabel;

    private void OnEnable()
    {
        string[] scores = PlayerPrefs.GetString("HighScores","").Split(',');

        string result = "";

        for (int i = 0; i < scores.Length; i++)
        {
            result += (i + 1) + ". " + scores[i] + "\n"; // "\n"은 줄바꿈 표시
        }

        ScoreLabel.text = result;

    }

    public void ClosePressed()
    {
        gameObject.SetActive(false);
    }
}
