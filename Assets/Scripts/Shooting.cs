using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPref;

    private float nextFireTime;

    public bool shotgun = false;
    public int bullets;
    public int spread;

    public float delay;

    ConcurrentBag<Quaternion> cp;
    List<Quaternion> pellets;

    public float bulletForce = 20f;

    private Vector2 pelletForce;
    private Animator myAnimator;


    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Awake()
    {
        if (shotgun)
        {
            //ConcurrentBag because I can't use lists in multiple threads even if I don't modify amount of elements within a list. Yes I did have to fix it. Yes I didn't know what a concurrent bag is before this.
            cp = new ConcurrentBag<Quaternion>();
            pellets = new List<Quaternion>(bullets);

            for (int i = 0; i < bullets; i++)
            {
                pellets.Add(Quaternion.Euler(Vector2.zero));
                cp.Add(Quaternion.Euler(Vector2.zero));
            }
        }

    }

    //Check every frame if the player didn't press the thing and when he did do stuff
    void Update()
    {
        processInputs();
    }

    //What kind of inputs we're checking
    void processInputs()
    {
        if(Input.GetButton("Fire1"))
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + delay;
            }
        }
    }

    //Create a bullet and Yeet it with bulletForce amount of force
    void Shoot()
    {
        if (shotgun)
        {
            //This makes bullet ammount of pellets spread out in 2 dimensions with a bit of a random spread defined within "spread"
            int i = 0;
            foreach(Quaternion quat in cp)
            {
                pellets[i] = Random.rotation;
                GameObject pellet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);
                pellet.transform.rotation = Quaternion.RotateTowards(pellet.transform.rotation, pellets[i], spread);
                pelletForce = new Vector2(0f, bulletForce);
                Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
                rb.AddForce(pellet.transform.rotation * pelletForce);
                i++;
            }
        }
        else
        {
            GameObject bullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        myAnimator.SetTrigger("Shoot");
    }
}
