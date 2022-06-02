using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject[] relevantEnemies;

    private void Start()
    {
        foreach(GameObject enemy in relevantEnemies)
        {
            enemy.GetComponent<Enemy_Behavior>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach(GameObject enemy in relevantEnemies)
        {
            enemy.GetComponent<Enemy_Behavior>().enabled = true;
            enemy.GetComponent<Enemy_Behavior>().theShowBegins();
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
