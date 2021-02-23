/*****************************************************************************
// File Name :         ShoppingList.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : Abstract class that defines functionality of the in-game shopping lists.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;

public abstract class ShoppingList : MonoBehaviour
{
    private Dictionary<System.Type, int> shoppingDictionary = new Dictionary<System.Type, int>();



    /// <summary>
    /// Adds an item to the shopping center's repository.
    /// </summary>
    /// <param name="itemType">The type of item to add.</param>
    public void AddItem(System.Type itemType)
    {
        if (!ShoppingHelper.IsOfTypeItem(itemType))
        {
            Debug.LogWarning(gameObject.name + ": " + itemType + " is not a valid Item and cannot be added to the shopping list.");
            return;
        }

        // If we already have this item in the shopping center, just update the amount we have in store.
        if (shoppingDictionary.ContainsKey(itemType))
            shoppingDictionary[itemType] += 1;

        // If we don't have this item type in the shopping center, add it and set the amount to 1.
        else
            shoppingDictionary.Add(itemType, 1);

        Debug.Log(gameObject.name + ": Added item " + itemType.ToString() + " to the list.\n" +
            "Quantity of item is now " + shoppingDictionary[itemType]);
    }

    /// <summary>
    /// Removes an item from the shopping center's repository.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public void RemoveItem(System.Type itemType)
    {
        if (!ShoppingHelper.IsOfTypeItem(itemType))
        {
            Debug.LogWarning(gameObject.name + ": " + itemType + " is not a valid Item and cannot be removed from the shopping list.");
            return;
        }


        if (shoppingDictionary.ContainsKey(itemType))
            shoppingDictionary[itemType] -= 1;
        else
            Debug.LogWarning(gameObject.name + ": Cannot remove item of type " + itemType + " because it is not in our shopping list!");

        Debug.Log(gameObject.name + ": Removed item " + itemType.ToString() + " to the list.\n" +
            "Quantity of item is now " + shoppingDictionary[itemType]);
    }
}
