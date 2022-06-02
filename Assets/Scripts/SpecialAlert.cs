using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAlert : MonoBehaviour
{
    public GameObject mega;
    public GameObject smol;
    public GameObject[] spawnPoints;
    public BoxCollider2D[] triggers;
    public GameObject[] blockades;
    public float waveTime;
    public int smolNum;
    public int megaNum;
    private int waveCout = 0;
    private float breather = 0f;

    public int remainingEnemies;
    private bool safety = false;
    private AudioManager audioManager;
    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].enabled = false;
            remainingEnemies = smolNum + megaNum;
        }

        for (int i = 0; i < blockades.Length; i++)
        {
            blockades[i].SetActive(true);
        }
        StartCoroutine(Survive(waveTime));
    }

    private void Update() {
        if(remainingEnemies <= 0 && safety)
        {
            if(waveCout == 0){
                breather = 3.5f;
                waveCout ++;
                StartCoroutine(Finish(breather));
            } else {
                Win();
            }
        }
    }

    private IEnumerator Survive(float timeWave)
    {
        safety = true;
        for (int i = 0; i < smolNum; i++)
        {
            Instantiate(smol, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < megaNum; i++)
        {
            Instantiate(mega, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(timeWave);
        smolNum ++;
        megaNum ++;
        waveCout ++;
    }

    private IEnumerator Finish(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < smolNum; i++)
        {
            Instantiate(smol, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
            
        }

        for (int i = 0; i < megaNum; i++)
        {
            Instantiate(mega, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(waveTime);
        Win();
    }

    void Win(){
        for (int i = 0; i < blockades.Length; i++)
        {
            blockades[i].SetActive(false);
        }
        audioManager.sounds[0].volume += 0.1625f;
    }
}
