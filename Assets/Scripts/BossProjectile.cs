using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int bulletSpeed;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(this.gameObject);

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Movement>().Damage(1);
        }
    }
}
