using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Behavior : MonoBehaviour
{
    //Variables
    public Transform target;

    public GameObject player;

    private Animator myAnimator;

    Vector3 rotation = new Vector3( 0f, 0f, -180f);

    public float speed;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    //Why the hell is this needed? I mean it just turns true -> false and the other way around and does nothing.
    bool reachedEndPath = false;
    //Yeah I don't completely understand A* yet. I assume this seeks out a path on the predetermined grid.
    Seeker seeker;

    //Enemy HP
    public float HP;

    //how much damage it deals
    public int damage;
    //Can player take damage. I'm actually unsure if this works properly atm.
    private bool health;
    //Our rigidbody
    private Rigidbody2D rb;
    public bool canDamage;
    private SpecialAlert headCount;
    //Why are we still here? Just to suffer? To apply knockback I need vector2. What do I need?
    /*1. Position of PC
    2. position of Enemy {Deprecated}
    3. Color Change on hit*/

    //Methods

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        myAnimator.enabled = false;    
        headCount = FindObjectOfType<SpecialAlert>();
    }
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        //since Rb is private because leaving it on public might cause issues with readability we get our rigid body through code
        rb = this.GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();


        // InvokeRepeating("UpdatePath", 0f, .5f);
    }
    public void theShowBegins()
    {
        myAnimator.enabled = true;
        StartCoroutine(Improvisation());
    }

    public IEnumerator Improvisation()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
        InvokeRepeating("UpdatePath", 0f, .5f);
        print("The Hunt begins");
    }

    void UpdatePath()
    {
            if(seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }    
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
    }

        void Move()
        {
            if (path == null)
                return;

            if(currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndPath = true;
                return;
            } else 
            {
                reachedEndPath = false;
            }

             Vector2 direction = (Vector2)path.vectorPath[currentWaypoint] - rb.position;
             direction.Normalize();
             Vector2 force = direction * speed * Time.deltaTime;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            rb.AddForce(force);

            if (distance < nextWaypointDistance) 
            {
                currentWaypoint++;
            }
        }
        void Rotate()
        {
            Vector2 lookDir = new Vector2(target.position.x, target.position.y) - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
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
            headCount.remainingEnemies--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //we check if the player can take damage and if the enemy has hatched
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            myAnimator.SetTrigger("Chomp");
            collision.gameObject.GetComponent<Movement>().Damage(damage);
            Debug.Log("-1 Health");
        }
    }
    public void HitIndicator()
    {
        StartCoroutine(HitCoroutine());  
    }

    public IEnumerator HitCoroutine()
    {
        float length = 0.3f;

        //Nothing made me act like an old man with dementia more than finally understanding that "new Color" instead of Color was the issue.
        SpriteRenderer spritey = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        spritey.color = new Color(1f,0f,0f,0.8f);

        yield return new WaitForSeconds(length);

        spritey.color = Color.white;
    }
}
