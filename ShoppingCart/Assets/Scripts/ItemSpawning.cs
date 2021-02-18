using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    //public List<SpawnPoint> spawnPoints;
    public GameObject[] spawnPoints; //spawn point objects must be manually set due to the complexity of the game map.
    //If we want to, this can be altered so that not every spawnpoint gets an item; up to the team
    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        //items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject s in spawnPoints)
        {
            int i = Random.Range(0, items.Length);
            Instantiate(items[i], s.transform);
        }
    }
}
