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
        if (GameManager.Instance.IsCleared) // IsCleared가 켜진 상태로 부름
        {
            SaveHighScore();
            /*
            float newrecord = PlayerPrefs.GetFloat("NewRecord", 0); // 이전 하이스코어 가져옴, 값이 없으면 0 처리
            if (newrecord < GameManager.Instance.TimeLimit) // 이전 점수보다 이번 점수가 높음
            {
                NewRecord.SetActive(true);

                PlayerPrefs.SetFloat("NewRecord", GameManager.Instance.TimeLimit); // 점수 갱신
                PlayerPrefs.Save(); // 세이브
            }
            else
            {
                NewRecord.SetActive(false);
            }
            */

            Time.timeScale = 0; //시간 정지
            TitleLabel.text = "Stage Clear!";
            ScoreLabel.text = "Score : " + GameManager.Instance.TimeLimit.ToString("#.##");
        }
        else // IsCleared가 꺼진 상태로 부름
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
            string[] scoreArray = savedScoreString.Split(","); // ,를 기점으로 숫자들을 나눠서 배열
            List<string> scoreList = new List<string>(scoreArray); // 위에 나눈 숫자들로 리스트 만들기

            for (int i = 0 ; i < scoreList.Count; i++)
            {
                float savedScore = float.Parse(scoreList[i]); // i번째 있는 string을 float으로 바꿔준다
                if(savedScore<score) // 자리 찾으면
                {
                    scoreList.Insert(i, currentScoreString); // 거기에 들어감
                    break;
                }
            }

            if(scoreArray.Length == scoreList.Count) // 리스트 다 훑어도 들어 갈 자리가 없음
            {
                scoreList.Add(currentScoreString); // 제일 끝에 넣기
            }
            if (scoreList.Count > 10) // 10위 안에 못들어감
            {
                scoreList.RemoveAt(10); // 11번째를 제거
            }

            string result = string.Join(",", scoreList); // ,를 기점으로 숫자들을 다시 합침
            PlayerPrefs.SetString("HighScores", result);
        }

 //       Debug.Log(PlayerPrefs.GetString("HighScores"));
        PlayerPrefs.Save();
    }

    public void PlayAgainPressed()
    {
        Time.timeScale = 1; //시간 재생
        SceneManager.LoadScene("GameScene");
    }

    public void HighScorePressed()
    {
        HighScorePopup.SetActive(true);
    }
}
