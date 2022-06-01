using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    //This script is old and I don't have time to make it look and work well (Or rather remake it because at least half of this should be rewriten) 
    //so now we both get to suffer
    //What we collide with while dashing
    [SerializeField] LayerMask dashInclude;

    public Rigidbody2D rb;
    public Camera cam;
    private GameObject player;
    public RawImage Icon;
    public Text healthPoints;
    public Gradient gradient;
    public Image fill;

    //How fast we're moving
    public float moveSpeed;
    //To keep it simple this is how far we're dashing
    public float dashRange;
    //How long are the iFrames gonna last
    public float invFrames;
    public float moveX;
    public float moveY;

    //Quality of life     To be Deleted
    private bool Ded = false;
    //If this is false we're not dashing
    public bool dash;
    // Self explanatory. Delay between dashes.
    public float dashDelay;

    private Animator myAnimator;

    private float delTime;
    //Are we taking damage
    public bool HP = true;
    //how much health we have
    public float health;
    private float maxHealth;

    public Slider healthBar;

    private Vector2 moveDirection;
    private Vector2 mousePos;

    public bool currentDoorKey = false;
    private AudioManager audioManager;

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        cam = FindObjectOfType<Camera>();
        myAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = this.gameObject;
        maxHealth = health;
        healthPoints.text = "100%";
        fill.color = gradient.Evaluate(health/maxHealth);
    }

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
        //Stuff for the upcoming Animator stuff
        moveY = Input.GetAxisRaw("Horizontal");
        moveX = Input.GetAxisRaw("Vertical");

        //I didn't have much time and I linked animator stuff in a wrong way at first. Long story short I'm not sure it's needed but I don't have time and the balls to fuck around.
        bool failSafe = true;

        //if because the movement needs animation and I couldn't reasonably fit animator stuff somewhere else
        if (Mathf.Abs(moveX) == 1f || Mathf.Abs(moveY) == 1f && failSafe == true)
        {
            myAnimator.SetBool("isMoving", true);
            failSafe = false;
        } else
        {
            myAnimator.SetBool("isMoving", false);
            failSafe = true;
        }

        //Where are we moving
        moveDirection = new Vector2(moveY, moveX).normalized;

        //Where the mouse is
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        if (delTime <= Time.time)
        {
            Icon.color = Color.white;
        }

        //We dashin here
        if (Input.GetKeyDown(KeyCode.Space) && delTime <= Time.time)
        {
            dash = true;
        }
    }

    void Move()
    {
        //I mean yeah we move the thing that this script is linked to. Preferably a player character.
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        //haphazardly made camera code so stuff doesn't break
        cam.transform.position = (new Vector3 (player.transform.position.x, player.transform.position.y, -10f));

        //camera for challange rooms? With a bit of tweaking this could be rather nice.
        //cam.transform.position = (new Vector3 (player.transform.position.x, player.transform.position.y, -10f) * moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        //We rotate based on camera or rather mouse orientation with camera... Yes that means I couldn't put camera in players children. Yes it makes everything confusing.
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Dash()
    {
        if (dash)
        {
            audioManager.Play("dash");

            Icon.color = new Color(1,1,1,0.5f);

            myAnimator.SetTrigger("isDashing");

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

            //We use it earlier
            delTime = Time.time + dashDelay;

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

    public void Damage(int damage)
    {
        if(HP)
        {
            health -= damage;
            print("Current Health: " + health);
            StartCoroutine(iFrames());
            myAnimator.SetTrigger("Hit");
            HealthFill();
        }
    }

    //I honestly have no idea what the hell is this supposed to be but I'm afraid of deleting it.
    void life()
    {
        if(health <= 0)
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

    void HealthFill()
    {
        healthBar.value = health/maxHealth;
        healthPoints.text = (health/maxHealth * 100).ToString() + "%";
        fill.color = gradient.Evaluate(health/maxHealth);
    }
}
