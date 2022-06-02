using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAlert : MonoBehaviour
{
    public float survivalTime;
    public GameObject mega;
    public GameObject smol;
    public GameObject[] spawnPoints;
    public BoxCollider2D[] triggers;
    public float waveTime;
    public int smolNum;
    public int megaNum;

    public int remainingEnemies;
    private bool safety = false;
    private void OnTriggerEnter2D(Collider2D other) {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].enabled = false;
            remainingEnemies = smolNum + megaNum;
        }
        StartCoroutine(Survive(waveTime));
    }

    private void Update() {
        if(remainingEnemies <= 0 && safety)
        {

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
    }
}
