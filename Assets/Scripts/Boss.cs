using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    /*Things we need:
    1. Each part of boss has to have health. This will be a problem. Maybe?
    2. Boss attacks. We don't talk about this. Class = BossAttacks.
    3. Death State (Odpala się odpowiednia animacja wybuchy, wytryski, eksplozje) (We'll get that one working later)
    4. Something to switch between attacks (random ammount of time x-y after that next attack comes.)
    
    1. (Done)
    2. Call animator trigger. Animation has a trigger that calls the attack method that instantiates projectiles / casts Raycasts.(Done)
    3. When the health runs out boss has to go into right animation sate and we'll play some particles or something like that
    4. Boss has 3 atacks if I make him cast them at random with random intervals gameplay will get messy. (State Machine?)
        Let's designate like 3 sets of coroutines / methods that will serve us as patterns and then we can choose a pattern > set time grace period in wich
        PC can do whatever they want > another pattern*/

    //Things we need to define right now
    public float healthLhand;
    public float healthRhand;
    public float healthHead;
    public float betweenPatterns;
    private bool head = false;
    private bool hand0 = false;
    private bool hand1 = false;
    public List<GameObject> children;
    private int damagae = 2;

    //DO NOT TOUCH UNTILL YOU KNOW WHAT IT'S USED FOR
    //x: lower number of rng range INCLUSIVE
    //y: higher number of rng range EXCLUSIVE (We're rouding it to the closes int so basic definition doesn't apply)
    //Default values in case someone was "testing stuff" and forgott:
    //x: 0
    //y: 2
    private float x = 0;
    private float y = 2;

    private Animator myAnimator;
    public GameObject winScreen;

    private void Awake() {
        myAnimator = this.GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(DownTime());
        foreach (Transform child in this.gameObject.transform)
        {
            children.Add(child.gameObject);
        }
    }

    private void Update() {
        //0: LArm 1: RArm 2:Head
        if(healthLhand <= 0 && hand0 == false)
        {
            hand0 = true;
            Death(0);
        }
        if(healthRhand <= 0 && hand1 == false)
        {
            hand1 = true;
            Death(1);
        }
        if(healthHead <= 0 && head == false)
        {
            head = true;
            Death(2);
        }
        if(head == true && hand0 == true && hand1 == true)
        {
            StopAllCoroutines();
        }
    }
    private void Death(int num){
        //0: LArm 1: RArm 2:Head
        int deathCount = 0;
        switch (num)
        {
            case 0:
                children[0].GetComponent<SpriteRenderer>().color = Color.gray;
                children[0].GetComponent<BoxCollider2D>().enabled = false;
                deathCount ++;
                break;
            
            case 1:
                children[1].GetComponent<SpriteRenderer>().color = Color.gray;
                children[1].GetComponent<BoxCollider2D>().enabled = false;
                deathCount ++;
                break;

            case 2:
                children[2].GetComponent<SpriteRenderer>().color = Color.gray;
                children[2].GetComponent<BoxCollider2D>().enabled = false;
                deathCount ++;
                break;
            
            default:
            return;
        }

        x = 2;
        y = 5;
        if (deathCount >= 3)
        {
            winScreen.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<Movement>().Damage(damagae);
    }

    private IEnumerator DownTime()
    {
        //We randomly choose what gets into that switch down there.
        int pattern = (int)Mathf.Round(Random.Range(x,y));
        print("Random Chose Pattern:" + pattern);
        yield return new WaitForSeconds(betweenPatterns);
        
        switch (pattern)
        {
            case 0:
                StartCoroutine(LRL());
                break;
            
            case 1:
                StartCoroutine(RRL());
                break;
            
            case 2:
                StartCoroutine(LHL());
                break;
            
            case 3:
                StartCoroutine(RLLR());
                break;

            default:
                StartCoroutine(DownTime());
                break;
        }
    }
    //L: Left Hand = myAnimator.SetTrigger("LeftAttack")
    //R: Right Hand = myAnimator.SetTrigger("RightAttack")
    //H: Head = myAnimator.SetTrigger("Laser")
    public IEnumerator LRL()
    {
        myAnimator.SetTrigger("LeftAttack");
        yield return new WaitForSeconds(1.5f);
        myAnimator.SetTrigger("RightAttack");
        yield return new WaitForSeconds(1.5f);
        myAnimator.SetTrigger("LeftAttack");
        StartCoroutine(DownTime());
    }

    public IEnumerator LHL()
    {
        myAnimator.SetTrigger("LeftAttack");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("Laser");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("LeftAttack");
        StartCoroutine(DownTime());
    }

    public IEnumerator RRL()
    {
        myAnimator.SetTrigger("RightAttack");
        yield return new WaitForSeconds(1.5f);
        myAnimator.SetTrigger("RightAttack");
        yield return new WaitForSeconds(1.5f);
        myAnimator.SetTrigger("LeftAttack");
        StartCoroutine(DownTime());
    }

    public IEnumerator RLLR()
    {
        myAnimator.SetTrigger("RightAttack");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("LeftAttack");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("LeftAttack");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("RightAttack");
        StartCoroutine(DownTime());
    }

    public IEnumerator HRLH()
    {
        myAnimator.SetTrigger("Laser");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("RightAttack");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("LeftAttack");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("Laser");
        StartCoroutine(DownTime());
    }
}
