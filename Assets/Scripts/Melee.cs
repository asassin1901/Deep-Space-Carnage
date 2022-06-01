using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Melee : MonoBehaviour
{
    public Animator thisAnimator;
    public Transform attackPoint;
    private bool safe = false;

    public float attackRange = 1.5f;
    public int damage;
    public float delay;
    private float afterDelay;
    public RawImage Icon;

    public LayerMask enemyLayers;

    private void Awake() {
        thisAnimator = GetComponent<Animator>();     
    }
    void Update()
    {
        if(Time.time >= afterDelay)
        {
            Icon.color = Color.white;
        }
        if(Input.GetButton("Fire2") && Time.time >= afterDelay)
        {
            Swing();
        }
    }

    void Swing()
    {
        thisAnimator.SetTrigger("Strike");

        Icon.color = new Color(1,1,1,0.5f);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy_Behavior>().HP -= damage;
        }
        
        afterDelay = Time.time + delay;
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
