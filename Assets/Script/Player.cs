using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public float Speed = 4f;
    public float JumpForce = 15f;
    public Collider2D Feet; //�߹ٴڿ� �ִ� �ݶ��̴�
    public CompositeCollider2D TerrainCollider; // ���� �ݶ��̴�


    float vx = 0; //���� �ӵ�
    float PrevVx = 0; //���� �ӵ�
    public bool IsGround;
    float LastShoot; // �Ѿ� ��Ÿ�� �ְ������

    Vector2 OriginPosition;
    void Start()
    {
        OriginPosition = transform.position;
    }

    public void Restart()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // Z�� ������
        GetComponent<Rigidbody2D>().angularVelocity = 0; //ȸ�� ����
        GetComponent<BoxCollider2D>().enabled = true;

        transform.eulerAngles = Vector3.zero; //ȸ�� ���� // eulerAngles ȸ�� ����
        transform.position = OriginPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //�ӵ� ���η� �����

    }
    // Update is called once per frame
    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal"); // GetAxisRaw GetAxis ó�� �����̳� ���ӵ� ���� �ٷ� ���� ������

        if (vx < 0) //�������� ��������
        {
            GetComponent<SpriteRenderer>().flipX = true; //��������Ʈ X�� ������
        }
        if (vx > 0) //���������� ��������
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Feet.IsTouching(TerrainCollider)) //Feet�� TerrainCollider�� �������� //���� �پ�����
        {
            if(!IsGround) //��� �� ������ �ƴϾ��� // ����
            {
                if (vx ==0) // �̵��� 0�� ��
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else // �̵��� 0�� �ƴ� ��
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }
            }

            else//��� �پ����� // �ȴ� ��
            {
                if (PrevVx != vx) 
                {
                    if (vx == 0) 
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else // �̵��� 0�� �ƴ� ��
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            IsGround = true;
        }
         else //���� �Ⱥپ�����
        {
            if(IsGround)//��� �� ������ �پ��־��� // ���� ����
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
                                     // && ���Ĵ� �Ѿ� ��Ÿ�� �ְ������
        if (Input.GetKeyDown(KeyCode.Z) && LastShoot + 0.1f < Time.time) // ���������� �߻��� �ð����� �ּ� .1�� ���Ŀ�
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

            GameObject Bullet = ObjectPool.Instance.GetBullet(); //�Ѿ� ������
            Bullet.transform.position = transform.position;
            Bullet.GetComponent<Bullet>().Velocity = BulletV;

            // �Ѿ� ��Ÿ�� �ְ������
            LastShoot = Time.time; // Input Ű�� ���� �ð� ���

        }
    }



    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * vx * Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") // ���� ���˽� �������
        {
            Die();
        }
    }

    void Die()
    {
        // ������ ���ۺ��� ���鼭 ������
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Z�� ������ ����
        GetComponent<Rigidbody2D>().angularVelocity = 720; //ȸ�� �ο�
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10),ForceMode2D.Impulse); // ƨ�� ���ư��� �� �ο� // ForceMode2D.Impulse �ѹ濡 �� �� �ϰ� �ִ� ��
        GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.Die();
    }
}
