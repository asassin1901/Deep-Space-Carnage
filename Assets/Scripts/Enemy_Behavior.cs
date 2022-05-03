using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Behavior : MonoBehaviour
{
    //Enemy HP
    public float HP;

    //how much damage it deals
    public float damage;
    //Can player take damage
    private bool health;

    public object playerScript;

    private Rigidbody2D rb;

    public void Start()
    {
        //since Rb is private because leaving it on public might cause issues with readability we get our rigid body through code
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        //We check if objects HP reached 0
        death();

        if (health)
        {
            //we deal damage
            FindObjectOfType<Movement>().health -= damage;
            health = false;
        }
    }

    private void death()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //we check if the player can take damage
        if (collision.gameObject.CompareTag("Player"))
        {
            health = collision.gameObject.GetComponent<Movement>().HP;
            Debug.Log("-1 Health");
        }
    }
}
