using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed = 4f;
    public float JumpForce = 15f;
    public Collider2D Feet; //발바닥에 있는 콜라이더
    public CompositeCollider2D TerrainCollider; // 지형 콜라이더


    float vx = 0; //현재 속도
    float PrevVx = 0; //이전 속도
    public bool IsGround;
    float LastShoot; // 총알 쿨타임 넣고싶으면

    Vector2 OriginPosition;
    void Start()
    {
        OriginPosition = transform.position;
    }

    public void Restart()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // Z축 프리즈
        GetComponent<Rigidbody2D>().angularVelocity = 0; //회전 삭제
        GetComponent<BoxCollider2D>().enabled = true;

        transform.eulerAngles = Vector3.zero; //회전 제로 // eulerAngles 회전 정보
        transform.position = OriginPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //속도 제로로 만들기

    }
    // Update is called once per frame
    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal"); // GetAxisRaw GetAxis 처럼 관성이나 가속도 없이 바로 딱딱 움직임

        if (vx < 0) //왼쪽으로 가고있음
        {
            GetComponent<SpriteRenderer>().flipX = true; //스프라이트 X축 뒤집기
        }
        if (vx > 0) //오른쪽으로 가고있음
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Feet.IsTouching(TerrainCollider)) //Feet가 TerrainCollider와 접촉중임 //땅에 붙어있음
        {
            if(!IsGround) //방금 전 까지는 아니었음 // 착지
            {
                if (vx ==0) // 이동이 0일 때
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else // 이동이 0이 아닐 떄
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }
            }

            else//계속 붙어있음 // 걷는 중
            {
                if (PrevVx != vx) 
                {
                    if (vx == 0) 
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else // 이동이 0이 아닐 떄
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            IsGround = true;
        }
         else //땅에 안붙어있음
        {
            if(IsGround)//방금 전 까지는 붙어있었음 // 점프 시작
            {
                GetComponent<Animator>().SetTrigger("Jump");
            }
            IsGround = false;
        }


        if (Input.GetButtonDown("Jump") && IsGround)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpForce;
        }

        PrevVx = vx;
                                     // && 이후는 총알 쿨타임 넣고싶으면
        if (Input.GetKeyDown(KeyCode.Z) && LastShoot + 0.1f < Time.time) // 마지막으로 발사한 시간에서 최소 .1초 이후에
        {
            Vector2 BulletV = Vector2.zero;

            if (GetComponent<SpriteRenderer>().flipX)
            {
                BulletV = new Vector2(-10,0);
            }
            else 
            {
                BulletV = new Vector2(10, 0);
            }

            GameObject Bullet = ObjectPool.Instance.GetBullet(); //총알 얻어오기
            Bullet.transform.position = transform.position;
            Bullet.GetComponent<Bullet>().Velocity = BulletV;

            // 총알 쿨타임 넣고싶으면
            LastShoot = Time.time; // Input 키를 누른 시간 기록

        }
    }



    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * vx * Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") // 적과 접촉시 사망판정
        {
            Die();
        }
    }

    void Die()
    {
        // 죽으면 빙글빙글 돌면서 떨어짐
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Z축 프리즈 해제
        GetComponent<Rigidbody2D>().angularVelocity = 720; //회전 부여
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10),ForceMode2D.Impulse); // 튕겨 날아가는 힘 부여 // ForceMode2D.Impulse 한방에 힘 빵 하고 주는 거
        GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.Die();
    }
}
