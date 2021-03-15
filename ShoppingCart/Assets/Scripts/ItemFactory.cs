/*****************************************************************************
// File Name :         ItemFactory.cs
// Author :            Kyle Grenier
// Creation Date :     03/06/2021
//
// Brief Description : Manages retrieving an item prefab from the Resources folder.
*****************************************************************************/
using UnityEngine;

public static class ItemFactory
{
    /// <summary>
    /// Returns a prefab of the item to spawn.
    /// </summary>
    /// <param name="itemName">The exact name of the item prefab in the Resources folder.</param>
    /// <returns>A prefab of the item to spawn.</returns>
    public static GameObject Spawn(string itemName)
    {
        // Retrieving the item prefab.
        GameObject prefab = Resources.Load(itemName) as GameObject;

        if (prefab == null)
        {
            Debug.LogWarning("ItemFactory: Cannot find a prefab named '" + itemName + "' in the Resources folder! Defaulting to the blue cube...");
            prefab = Resources.Load("LooseItem") as GameObject;
        }

        return prefab;
    }
}
