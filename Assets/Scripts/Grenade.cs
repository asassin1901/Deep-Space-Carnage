using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPoint;

    public float attackRange;
    public int damage;
    public GameObject hitEffect;
    public float effect_time = 2.5f;

    public LayerMask enemyLayers;
    private void OnCollisionEnter2D(Collision2D other) {
        print("boop");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        GameObject effect = Instantiate(hitEffect, transform.position, gameObject.transform.rotation);
        Destroy(effect, effect_time);
        Destroy(gameObject);
        foreach (Collider2D enemy in hitEnemies)
        {
            switch (enemy.gameObject.tag)
            {
                case "HandL":
                    enemy.gameObject.GetComponentInParent<Boss>().healthLhand -= damage;
                    print("Left Hand");
                    break;

                case "HandR":
                    enemy.gameObject.GetComponentInParent<Boss>().healthRhand -= damage;
                    print("Right Hand");
                    break;

                case "Head":
                    enemy.gameObject.GetComponentInParent<Boss>().healthHead -= damage;
                    print("Head");
                    break;

                case "Enemy":
                    enemy.gameObject.GetComponent<Enemy_Behavior>().HP -= damage;
                    enemy.gameObject.GetComponent<Enemy_Behavior>().HitIndicator();
                    break;

                default:

                    break;
            }
        }

    }
    
}
