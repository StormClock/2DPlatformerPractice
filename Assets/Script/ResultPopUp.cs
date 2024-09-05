using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPopUp : MonoBehaviour
{

    public TextMeshProUGUI TitleLabel;
    public TextMeshProUGUI ScoreLabel;
    public GameObject HighScorePopup;
    //public GameObject NewRecord;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (GameManager.Instance.IsCleared) // IsCleared�� ���� ���·� �θ�
        {
            SaveHighScore();
            /*
            float newrecord = PlayerPrefs.GetFloat("NewRecord", 0); // ���� ���̽��ھ� ������, ���� ������ 0 ó��
            if (newrecord < GameManager.Instance.TimeLimit) // ���� �������� �̹� ������ ����
            {
                NewRecord.SetActive(true);

                PlayerPrefs.SetFloat("NewRecord", GameManager.Instance.TimeLimit); // ���� ����
                PlayerPrefs.Save(); // ���̺�
            }
            else
            {
                NewRecord.SetActive(false);
            }
            */

            Time.timeScale = 0; //�ð� ����
            TitleLabel.text = "Stage Clear!";
            ScoreLabel.text = "Score : " + GameManager.Instance.TimeLimit.ToString("#.##");
        }
        else // IsCleared�� ���� ���·� �θ�
        { 
            TitleLabel.text = "Game Over";
            ScoreLabel.text = "";
        }
    }

    void SaveHighScore()
    {
        float score = GameManager.Instance.TimeLimit;
        string currentScoreString = score.ToString("#.###");

        string savedScoreString = PlayerPrefs.GetString("HighScores", "");

        if(savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(","); // ,�� �������� ���ڵ��� ������ �迭
            List<string> scoreList = new List<string>(scoreArray); // ���� ���� ���ڵ�� ����Ʈ �����

            for (int i = 0 ; i < scoreList.Count; i++)
            {
                float savedScore = float.Parse(scoreList[i]); // i��° �ִ� string�� float���� �ٲ��ش�
                if(savedScore<score) // �ڸ� ã����
                {
                    scoreList.Insert(i, currentScoreString); // �ű⿡ ��
                    break;
                }
            }

            if(scoreArray.Length == scoreList.Count) // ����Ʈ �� �Ⱦ ��� �� �ڸ��� ����
            {
                scoreList.Add(currentScoreString); // ���� ���� �ֱ�
            }
            if (scoreList.Count > 10) // 10�� �ȿ� ����
            {
                scoreList.RemoveAt(10); // 11��°�� ����
            }

            string result = string.Join(",", scoreList); // ,�� �������� ���ڵ��� �ٽ� ��ħ
            PlayerPrefs.SetString("HighScores", result);
        }

 //       Debug.Log(PlayerPrefs.GetString("HighScores"));
        PlayerPrefs.Save();
    }

    public void PlayAgainPressed()
    {
        Time.timeScale = 1; //�ð� ���
        SceneManager.LoadScene("GameScene");
    }

    public void HighScorePressed()
    {
        HighScorePopup.SetActive(true);
    }
}
