using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;

    //This is a bit complicated
    private Vector2 movement;

    //How fast is this guy moving
    public float moveSpeed;

    //Enemy HP
    public float HP;

    //how much damage it deals
    public float damage;
    //Can player take damage
    private bool health;

    public void Start()
    {
        //since rb is private because leaving it on public might cause issues we get our rigid body through code
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        //We check the distance between player character and this object here
        Vector3 direction = player.position - transform.position;
        //We get the exact angle here
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //We rotate the object here
        rb.rotation = angle;
        //Movement shenanigans
        direction.Normalize();
        movement = direction;

        //We check if objects HP reached 0
        death();

        if (health == true)
        {
            //we deal damage
            FindObjectOfType<Movement>().health -= damage;
            health = false;
        }
    }

    private void FixedUpdate()
    {
        //We move our character here
        moveCharacter(movement);
    }

    public void moveCharacter(Vector2 direction)
    {
        //We get the direction and move the object here.
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
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
