using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Slider slider;

    public bool survivalDone = true;
    public bool playerDistance = false;

    public GameObject spawner;
    public Image bar;
    public Collider2D trigger;

    private float Timer;
    private float longTime;
    public float waitTime;

    void Start()
    {
        slider.value = 0f;
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

            playerDistance = false;

            trigger.enabled = false;

            longTime = Time.time + waitTime;
        }
        if (longTime >= Time.time)
        {
            survivalDone = true;
            Timer = longTime - Time.time;
            float test = 1 - (Timer/waitTime);

                if(spawner.activeInHierarchy == true)
            slider.value = test;
        } else 
        {
            bar.color = new Color(0,200,28);
            spawner.SetActive(false);
            survivalDone = false;
        }
    }
}
