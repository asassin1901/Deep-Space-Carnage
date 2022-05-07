using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Slider slider;

    public bool playerDistance = false;

    public GameObject spawner;
    public GameObject door;
    public Collider2D trigger;

    private float Timer;
    private float longTime;
    public float waitTime;

    void Start()
    {
        slider.value = 1;
        longTime = 0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDistance = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDistance = false;
        }
    }

    void Update()
    {
        if(playerDistance && Input.GetKeyDown(KeyCode.F))
        {
            spawner.SetActive(true);

            trigger.enabled = false;

            longTime = Time.time + waitTime;
        }
        if (longTime >= Time.time)
        {
            Timer = longTime - Time.time;
            float test = 1 - (Timer/waitTime);
            Debug.Log(test);

                if(spawner.activeInHierarchy == true)
            slider.value = test;
        } else 
        {
            spawner.SetActive(false);
        }
    }
}
