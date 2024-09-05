using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject BulletPrefab; // 이놈을 쟁겨놓을거다
    public int BulletLimit = 30; // 이만큼 쟁겨놓을거다

    List<GameObject> Bullets;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Bullets = new List<GameObject>();

        for (int i = 0; i < BulletLimit; i++) // 0에서 BulletLimit까지 만들어두기
        {
            GameObject go = Instantiate(BulletPrefab,transform);// transform 넣으면 이 컴포넌트의 자식으로 들어감
            go.SetActive(false);
        }
    }

    public GameObject GetBullet() //총알 배포
    {
        foreach (GameObject go in Bullets)
        { 
            if(!go.activeSelf) //액티브 안된 놈으로
            {
                go.SetActive(true);
                return go;
            }
        }
        // 있는 거 다 써서 남은 게 없을 때
        // return null; 

        // 남은 게 없으면 추가 발주
        GameObject obj = Instantiate(BulletPrefab,transform);
        Bullets.Add(obj);
        return obj;
    }
}
