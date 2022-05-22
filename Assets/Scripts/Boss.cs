using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    /*Things we need:
    1. Boss has to have health (Done?)/ I need to decide wether each part has health and fight gets harder with each part dead or whole boss = whole health
    2. Boss has to have his attacks: slam with the circle of projectiles(DONE), breath?(reuse the logic behind slam with lower spread and more projectiles),
        Swipe with the other hand?(anim/transform.translate/rb.AddRelativeForce(), RB2D with a collider!)
    3. Death State (Odpala się odpowiednia animacja wybuchy, wytryski, eksplozje) (We'll get that one working later)
    4. Something to switch between attacks (random ammount of time x-y after that next attack comes.)
    
    1. Yes I know trully a magnificent discovery
    2. When the method is called the right body part should head to the first point (x) and then to the second point (y) after wich we instantiate projectiles
        often with a for loop. Probably
    3. When the health runs out boss has to go into right animation sate and we'll play some particles or something like that
    4. After x ammount of time where x will be randomly generated in between attacks with another method probably. We'll also choose wich atack will be executed
        With RNG since we're planning like 3 attacks it's not enough to make patterns a good idea. (I think? What if we randomly choose a pattern of attacks?
        with a cooldown period in wich PC will be able to dish out dmg easily without having to bullet hell the shit out of it?)*/

    //Things we need to define right now
    public int health;//Without defining it to tweak easily during testing

}
