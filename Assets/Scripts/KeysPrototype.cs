using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysPrototype : MonoBehaviour
{
    string keyColor;
    /*To Do:
    If we have access to multiple:
    1. set up several bools.
    2. Check wich key has been collected and enable appropriate interaction
    3. You can only open door #1 with key #1
    
    If we have access to one:
    1. check if key was picked up
    2. If key is picked up allow for interaction with door.*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Movement>().currentDoorKey = true;
        gameObject.SetActive(false);
    }
}
