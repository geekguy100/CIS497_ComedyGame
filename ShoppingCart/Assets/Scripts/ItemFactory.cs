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
            Debug.LogWarning("ItemFactory: Cannot find a prefab named '" + itemName + "' in the Resources folder!");
            return null;
        }

        // Checking to see if it has the ItemContainer component. If it does not,
        // add the component to it.
        ItemContainer container = prefab.GetComponent<ItemContainer>();
        if (container == null)
            prefab.AddComponent<ItemContainer>();

        return prefab;
    }
}
