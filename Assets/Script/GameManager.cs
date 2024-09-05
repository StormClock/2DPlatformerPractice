using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //���ھ �� �� �ʿ�

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Player Player;
    public LifeControl LifeControl;
    public GameObject VirtualCamera;
    public GameObject PopupMenu;

    public TextMeshProUGUI ScoreLabel; //���ھ �� �� �ʿ�
    public float TimeLimit = 80f;
    public int Life = 3;

    public bool IsCleared;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        LifeControl.SetLife(Life); //LifeControl�� SetLife �� Life ��ŭ ����
        IsCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLimit -= Time.deltaTime;
     //   ScoreLabel.text = ((int)TimeLimit).ToString(); // ScoreLabel�� TimeLimit�� int �������� ToString���ڷ� ǥ��
        ScoreLabel.text = "Time Left : " + TimeLimit.ToString("#.##"); // "Time Left : " �ڿ�  "#.##" �Ҽ��� �Ʒ� ���ڸ����� ToString���ڷ� ǥ��

    }

    public void AddTime(float time)
    {
        TimeLimit += time;
    }

    public void Die() 
    {
        VirtualCamera.SetActive(false); //VirtualCamera ������Ʈ �������

        Life--;
        LifeControl.SetLife(Life);//LifeControl�� SetLife �� Life ����
        Invoke("Restart", 2); //"Restart"�� 2�� �Ŀ� ����

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
            VirtualCamera.SetActive(true);//VirtualCamera ������Ʈ ����
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
