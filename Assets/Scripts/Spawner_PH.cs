using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PH : MonoBehaviour
{
    private float delay = .5f;
    private float afterDelay;
    public GameObject entityToSpawn;

    public SpawnManagerScriptableObject spawnManagerValues;

    public int instanceNumber = 0;

    public bool survival = false;

    private float survivalCountdown;

    public float survivalTime;

    private int howMany;

    List<GameObject> spawners = new List<GameObject>();
    void Start()
    {
        howMany = spawnManagerValues.numberOfPrefabsToCreate;

        survivalCountdown = Time.time + survivalTime;
        for (int i = 0; i < howMany; i++)
        {
            spawners.Add(new GameObject("Spawner " + i));
            spawners[i].transform.SetParent(gameObject.transform);
            spawners[i].transform.localPosition = spawnManagerValues.spawnPoints[i];
        }
    }
    
    void Update()
    {
        if(survival)
        {
            if(Time.time >= afterDelay)
                SpawnEntities();
        } 
        else
        {         
            if(Time.time >= afterDelay && instanceNumber <= 10)
                SpawnEntities();
        }
    }

    void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < spawnManagerValues.numberOfPrefabsToCreate; i++)
        {
            // Creates an instance of the prefab at the current spawn point.
            GameObject currentEntity = Instantiate (entityToSpawn, spawners[i].transform, false);

            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
            currentEntity.name = spawnManagerValues.prefabName + instanceNumber;

            // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;

            instanceNumber++;
        }
        afterDelay = Time.time + delay;
    }
}