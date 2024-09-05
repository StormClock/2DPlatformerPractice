using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject BulletPrefab; // �̳��� ��ܳ����Ŵ�
    public int BulletLimit = 30; // �̸�ŭ ��ܳ����Ŵ�

    List<GameObject> Bullets;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Bullets = new List<GameObject>();

        for (int i = 0; i < BulletLimit; i++) // 0���� BulletLimit���� �����α�
        {
            GameObject go = Instantiate(BulletPrefab,transform);// transform ������ �� ������Ʈ�� �ڽ����� ��
            go.SetActive(false);
        }
    }

    public GameObject GetBullet() //�Ѿ� ����
    {
        foreach (GameObject go in Bullets)
        { 
            if(!go.activeSelf) //��Ƽ�� �ȵ� ������
            {
                go.SetActive(true);
                return go;
            }
        }
        // �ִ� �� �� �Ἥ ���� �� ���� ��
        // return null; 

        // ���� �� ������ �߰� ����
        GameObject obj = Instantiate(BulletPrefab,transform);
        Bullets.Add(obj);
        return obj;
    }
}
