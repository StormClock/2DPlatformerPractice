using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Velocity = new Vector2(10,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Terrain") 
        { 
            gameObject.SetActive(false); //Ç®¸µÀº ¸¾´ë·Î ºÎ½¤Áö¸é ¾È µÊ
        }
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(1);
            gameObject.SetActive(false);
        }
    }
}
