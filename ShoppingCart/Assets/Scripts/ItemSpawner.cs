using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemFactory factory;
    public GameObject item;
    public GameObject[] spawnPoints;
    public int index;

    private void Start()
    {
        index = 0;
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        factory = GameObject.FindGameObjectWithTag("Factory").GetComponent<ItemFactory>();
        foreach (GameObject s in spawnPoints)
        {
            RequestSpawn("null");
        }
    }

    public void RequestSpawn(string type)
    {
        item = factory.Spawn(type);
        Instantiate(item, spawnPoints[index].transform.position, Quaternion.identity);
        index++;
    }
}
