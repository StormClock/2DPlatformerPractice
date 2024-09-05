using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //Player 태그 오브젝트가 접촉시 사망판정
        {
            GameManager.Instance.Die();
        }
    }
}
