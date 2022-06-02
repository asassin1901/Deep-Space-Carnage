using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage;
    public float effect_time = 2.5f;
    public bool Sniper = false;

    public int penRate = 4;
    public int penCount;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        penCount = 1;
        Destroy(gameObject, 1.5f);
    }

    private void Update()
    {
        if(penCount>penRate)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, gameObject.transform.rotation);
            Destroy(effect, effect_time);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            GameObject effect = Instantiate(hitEffect, transform.position, gameObject.transform.rotation);
            Destroy(effect, effect_time);
            Destroy(gameObject);

            switch (collision.gameObject.tag)
            {
                case "HandL":
                    collision.gameObject.GetComponentInParent<Boss>().healthLhand -= damage;
                    print("Left Hand");
                    break;

                case "HandR":
                    collision.gameObject.GetComponentInParent<Boss>().healthRhand -= damage;
                    print("Right Hand");
                    break;

                case "Head":
                    collision.gameObject.GetComponentInParent<Boss>().healthHead -= damage;
                    print("Head");
                    break;

                case "Enemy":
                    collision.gameObject.GetComponent<Enemy_Behavior>().HP -= damage;
                    collision.gameObject.GetComponent<Enemy_Behavior>().HitIndicator();
                    break;

                default:

                    break;
            }
    }

}
