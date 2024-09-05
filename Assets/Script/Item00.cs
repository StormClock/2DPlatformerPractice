using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item00 : MonoBehaviour
{
    public float TimeAdd = 10f; //늘어나는 시간


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.AddTime(TimeAdd); //GameManager의 AddTime로 TimeAdd만큼 늘려줘라
            GetComponent<Animator>().SetTrigger("Eat");
            Invoke("DestroyThis",0.4f); //"DestroyThis" 를 0.4초 이후에 실행
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

}

