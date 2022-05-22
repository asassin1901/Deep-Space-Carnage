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
}
