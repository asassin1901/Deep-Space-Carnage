using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Movement>().currentDoorKey == true)
        {
            other.gameObject.GetComponent<Movement>().currentDoorKey = false;
            gameObject.SetActive(false);
        }
    }
}