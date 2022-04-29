using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //What we collide with while dashing
    [SerializeField] LayerMask dashInclude;

    public Rigidbody2D rb;
    public Camera cam;

    //How fast we're moving
    public float moveSpeed;
    //To keep it simple this is how far we're dashing
    public float dashRange;
    //How long are the iFrames gonna last
    public float invFrames;

    //Quality of life     To be Deleted
    private bool Ded = false;
    //If this is false we're not dashing
    public bool dash;
    //Are we taking damage
    public bool HP = true;
    //how much health we have
    public float health;

    private Vector2 moveDirection;
    private Vector2 mousePos;

    //Update goes every frame
    void Update()
    {
        //We check if dude died here
        life();
        //We check if his buttons are pressed here
        ProcessInputs();
    }
    // We move and rotate player character here
    private void FixedUpdate()
    {
        Move();
        Rotate();
        Dash();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Vertical");
        float moveY = Input.GetAxisRaw("Horizontal");

        //Where are we moving
        moveDirection = new Vector2(moveY, moveX).normalized;

        //Where the mouse is
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //We dashin here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
        }
    }

    void Move()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void Rotate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Dash()
    {
        if (dash)
        {
            // bool for turning on and off our dmg counting
            HP = false;

            //Where we're ending up
            Vector2 dashPosition = rb.position + moveDirection * dashRange *Time.deltaTime;

            //Raycast to see if we're hiting any walls and a way to stop us from getting through walls
            RaycastHit2D colDetection = Physics2D.Raycast(rb.position, moveDirection, dashRange, dashInclude);
            if (colDetection.collider != null)
            {
                dashPosition = colDetection.point;
            }

            //We move our character
            rb.MovePosition(dashPosition);

            //Coroutine to actually time the iFrames
            StartCoroutine(iFrames());

            //Enabling dash again since we can only dash when this changes to true
            dash = false;
        }
    }

    IEnumerator iFrames()
    {
        HP = false;
        yield return new WaitForSeconds(invFrames);
        HP = true;
    }

    void life()
    {
        if(health == 0)
        {
            Ded = true;
        }

        if(Ded)
        {
            Destroy(gameObject);
            Debug.Log("Ded");
            Ded = false;
        }
    }
}
