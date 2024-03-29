﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject[] relevantEnemies;
    private AudioManager audioManager;
    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        foreach(GameObject enemy in relevantEnemies)
        {
            enemy.GetComponent<Enemy_Behavior>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject enemy in relevantEnemies)
            {
                enemy.GetComponent<Enemy_Behavior>().enabled = true;
                enemy.GetComponent<Enemy_Behavior>().theShowBegins();
                print("Awake");
            }
            GetComponent<BoxCollider2D>().enabled = false;
        }
        audioManager.sounds[0].volume += 0.0625f;
    }
}
