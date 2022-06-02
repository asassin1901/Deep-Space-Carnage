using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
   float healthReturn;
   private void OnTriggerEnter2D(Collider2D other) {
       healthReturn = other.gameObject.GetComponent<Movement>().maxHealth;
       other.gameObject.GetComponent<Movement>().health = healthReturn;
       other.gameObject.GetComponent<Movement>().HealthFill();
       Destroy(this.gameObject);
   }
}
