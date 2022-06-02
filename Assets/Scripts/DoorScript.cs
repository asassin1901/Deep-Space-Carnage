using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int keyID;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            if (other.gameObject.GetComponent<Movement>().doorKeys[keyID] == true)
            {
                gameObject.SetActive(false);
            }
    }
}