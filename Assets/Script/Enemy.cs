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
            if (!FrontBottomCollider.IsTouching(TerrainCollider)) // FrontBottomCollider�� TerrainCollider�� ������ �������
            {
                vx = -vx; //������� ����
                transform.localScale = new Vector2(-transform.localScale.x, 1); //������Ʈ ���� ��ȯ
            }
            if (FrontCollider00.IsTouching(TerrainCollider) || FrontCollider01.IsTouching(TerrainCollider))
            {
                vx = -vx; //������� ����
                transform.localScale = new Vector2(-transform.localScale.x, 1); //������Ʈ ���� ��ȯ
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
            // ������ ���ۺ��� ���鼭 ������
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Z�� ������ ����
            GetComponent<Rigidbody2D>().angularVelocity = 720; //ȸ�� �ο�
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse); // ƨ�� ���ư��� �� �ο� // ForceMode2D.Impulse �ѹ濡 �� �� �ϰ� �ִ� ��
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
