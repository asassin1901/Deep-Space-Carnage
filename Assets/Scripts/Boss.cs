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
    2. Call animator trigger. Animation has a trigger that calls the attack method that instantiates projectiles / casts Raycasts.
    3. When the health runs out boss has to go into right animation sate and we'll play some particles or something like that
    4. Boss has 3 atacks if I make him cast them at random with random intervals gameplay will get messy. (State Machine?)
        Let's designate like 3 sets of coroutines / methods that will serve us as patterns and then we can choose a pattern > set time grace period in wich
        PC can do whatever they want > another pattern*/

    //Things we need to define right now
    public float healthLhand;
    public float healthRhand;
    public float healthHead;
    public float betweenPatterns;

    private Animator myAnimator;
    // In case I need it later Depends on what anims I get
    // private Animator lHandAnim;
    // private Animator rHandAnim;
    // private Animator HeadAnim;

    private void Awake() {
        myAnimator = this.GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(DownTime());
    }

    private IEnumerator DownTime()
    {
        int pattern = (int)Mathf.Round(Random.Range(0,2));
        print("Random Chose Pattern:" + pattern);
        yield return new WaitForSeconds(betweenPatterns);
        
        switch (pattern)
        {
            case 0:
                StartCoroutine(LRL());
                break;
            
            case 1:
                StartCoroutine(LHL());
                break;
            
            default:
                StartCoroutine(DownTime());
                break;
        }
    }

    public IEnumerator LRL()
    {
        myAnimator.SetTrigger("LeftAttack");
        yield return new WaitForSeconds(1f);
        myAnimator.SetTrigger("RightAttack");
        yield return new WaitForSeconds(1f);
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
}
