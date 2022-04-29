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

        if (Sniper)
        {
            if(collision.gameObject.layer == 3)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, gameObject.transform.rotation);
                Destroy(effect, effect_time);
                Destroy(gameObject);
            }

            if (penCount <= penRate)
            {
                rb.AddForce(new Vector2(0f, penRate/penCount), ForceMode2D.Force);
                penCount++;
            }
        }
        else
        {
            GameObject effect = Instantiate(hitEffect, transform.position, gameObject.transform.rotation);
            Destroy(effect, effect_time);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy_Behavior>().HP -= damage;
        }
    }

}
