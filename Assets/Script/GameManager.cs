using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //스코어링 할 때 필요

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Player Player;
    public LifeControl LifeControl;
    public GameObject VirtualCamera;
    public GameObject PopupMenu;

    public TextMeshProUGUI ScoreLabel; //스코어링 할 때 필요
    public float TimeLimit = 80f;
    public int Life = 3;

    public bool IsCleared;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        LifeControl.SetLife(Life); //LifeControl의 SetLife 에 Life 만큼 충전
        IsCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLimit -= Time.deltaTime;
     //   ScoreLabel.text = ((int)TimeLimit).ToString(); // ScoreLabel에 TimeLimit를 int 형식으로 ToString문자로 표현
        ScoreLabel.text = "Time Left : " + TimeLimit.ToString("#.##"); // "Time Left : " 뒤에  "#.##" 소수점 아래 두자리까지 ToString문자로 표현

    }

    public void AddTime(float time)
    {
        TimeLimit += time;
    }

    public void Die() 
    {
        VirtualCamera.SetActive(false); //VirtualCamera 오브젝트 기능정지

        Life--;
        LifeControl.SetLife(Life);//LifeControl의 SetLife 에 Life 갱신
        Invoke("Restart", 2); //"Restart"를 2초 후에 실행

        Debug.Log("YOU DIED");

    }

    public void Restart() 
    {
        if (Life <= 0)
        {
            GameOver();
        }

        else
        {
            Player.Restart();
            VirtualCamera.SetActive(true);//VirtualCamera 오브젝트 재기능
        }
    }
    public void StageClear()
    {
        Debug.Log("Score : " + TimeLimit.ToString("#.##"));
        Debug.Log("Clear");
        IsCleared = true;
        PopupMenu.SetActive(true);
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        IsCleared = false;
        PopupMenu.SetActive(true);
    }
}
