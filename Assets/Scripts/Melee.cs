using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Animator thisAnimator;
    public Transform attackPoint;

    public float attackRange = 1.5f;
    public int damage;
    public float delay;
    private float afterDelay;

    public LayerMask enemyLayers;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Time.time >= afterDelay)
        {
            Swing();
        }
    }

    void Swing()
    {
        thisAnimator.SetTrigger("Strike");

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
