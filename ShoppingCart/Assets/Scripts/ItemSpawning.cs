using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    //public List<SpawnPoint> spawnPoints;
    public GameObject[] spawnPoints;
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        foreach (GameObject s in spawnPoints)
        {
            Instantiate(item, s.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
