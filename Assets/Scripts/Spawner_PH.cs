using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PH : MonoBehaviour
{
    public GameObject enemy;
    public float time;
    public float timeMultiplier;
    public float limit;

    private void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1 * timeMultiplier;

        if(time >= limit)
        {
            GameObject newEnemy = Instantiate(enemy);
            time = 0;
        }
    }
}
