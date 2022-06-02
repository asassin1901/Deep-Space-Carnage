using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    public BoxCollider2D trigger;
    public GameObject boss;
    public GameObject baricade;
    bool test = false;
    Movement script;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            test = true;
            trigger.enabled = false;
            boss.SetActive(true);
            baricade.SetActive(true);

            script = other.GetComponent<Movement>();
            other.GetComponent<Movement>().cam.orthographicSize += 7 * Time.deltaTime;
        }
    }
    private void Update() {
        if (test)
        {
            script.cam.orthographicSize += 7 * Time.deltaTime;
        }
        if (script.cam.orthographicSize >= 10)
        {
            test = false;
            script.cam.orthographicSize = 10;
        }
    }

}
