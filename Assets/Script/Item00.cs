using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item00 : MonoBehaviour
{
    public float TimeAdd = 10f; //�þ�� �ð�


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.AddTime(TimeAdd); //GameManager�� AddTime�� TimeAdd��ŭ �÷����
            GetComponent<Animator>().SetTrigger("Eat");
            Invoke("DestroyThis",0.4f); //"DestroyThis" �� 0.4�� ���Ŀ� ����
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

}

