using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PH : MonoBehaviour
{
    public GameObject entityToSpawn;
    private GameObject button;

    public SpawnManagerScriptableObject spawnManagerValues;


    public bool survival;
    private bool whyAmIHere = false;

    private float survivalCountdown;
    private float delay = .5f;
    public float survivalTime;
    private float afterDelay;


    public int instanceNumber = 0;
    private int howMany;

    List<GameObject> spawners = new List<GameObject>();
    void Start()
    {
        howMany = spawnManagerValues.numberOfPrefabsToCreate;

        button = GameObject.FindGameObjectWithTag("Button");

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
                Debug.Log("spawners");
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

        if (survival)
        {
            for (int i = 0; i < howMany; i++)
            {
                // Creates an instance of the prefab at the current spawn point.
                GameObject currentEntity = Instantiate (entityToSpawn, spawners[i].transform.position, Quaternion.identity);

                // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
                currentEntity.name = spawnManagerValues.prefabName + instanceNumber;

                // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
                currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;

                instanceNumber++;
                survival = button.GetComponent<Button>().survivalDone;
            }

            afterDelay = Time.time + delay;
        } else
        {
            for (int i = 0; i < spawnManagerValues.numberOfPrefabsToCreate; i++)
            {
                // Creates an instance of the prefab at the current spawn point.
                GameObject currentEntity = Instantiate (entityToSpawn, spawners[i].transform.position, Quaternion.identity);

                // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
                currentEntity.name = spawnManagerValues.prefabName + instanceNumber;

                // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
                currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;

                instanceNumber++;
            }
            afterDelay = Time.time + delay;
        }
    }
}