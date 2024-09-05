using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int HP = 3;
    public float Speed = 2f;
    public Collider2D FrontBottomCollider;
    public Collider2D FrontCollider00;
    public Collider2D FrontCollider01;
    public CompositeCollider2D TerrainCollider;
    bool ImDead; 

    Vector2 vx;


    // Start is called before the first frame update
    void Start()
    {
        ImDead = false; 
        vx =Vector2.right * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ImDead)
        {
            if (!FrontBottomCollider.IsTouching(TerrainCollider)) // FrontBottomCollider와 TerrainCollider의 접촉이 사라지면
            {
                vx = -vx; //진행방향 변경
                transform.localScale = new Vector2(-transform.localScale.x, 1); //오브젝트 방향 변환
            }
            if (FrontCollider00.IsTouching(TerrainCollider) || FrontCollider01.IsTouching(TerrainCollider))
            {
                vx = -vx; //진행방향 변경
                transform.localScale = new Vector2(-transform.localScale.x, 1); //오브젝트 방향 변환
            }
        }
        
    }

    private void FixedUpdate()
    {
        transform.Translate(vx * Time.fixedDeltaTime);
    }

    public void Hit(int damage) 
    {
        HP -= damage;
        if (HP <= 0)
        {
            // 죽으면 빙글빙글 돌면서 떨어짐
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Z축 프리즈 해제
            GetComponent<Rigidbody2D>().angularVelocity = 720; //회전 부여
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse); // 튕겨 날아가는 힘 부여 // ForceMode2D.Impulse 한방에 힘 빵 하고 주는 거
            GetComponent<BoxCollider2D>().enabled = false;
            ImDead = true;

            Invoke("DestroyThis",2.0f);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
