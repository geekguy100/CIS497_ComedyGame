using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public GameObject n;
    public GameObject itemToSpawn;

    public GameObject Spawn(string type)
    {
        itemToSpawn = null;

        if (type.Equals("null"))
        {
            itemToSpawn = n;
        }

        return itemToSpawn;
    }
}
