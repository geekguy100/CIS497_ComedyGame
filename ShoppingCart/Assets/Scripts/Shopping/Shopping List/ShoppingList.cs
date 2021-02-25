/*****************************************************************************
// File Name :         ShoppingList.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : Class that defines functionality of the in-game shopping lists - adding items, removing items, etc.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class ShoppingList : MonoBehaviour
{
    private Dictionary<System.Type, int> shoppingDictionary;

    public event Action<System.Type> OnItemAdded;
    public event Action<System.Type> OnItemRemoved;


    protected virtual void Awake()
    {
        shoppingDictionary = new Dictionary<System.Type, int>();
    }

    /// <summary>
    /// Adds an item to the shopping center's repository.
    /// </summary>
    /// <param name="itemType">The type of item to add.</param>
    public virtual void AddItem(System.Type itemType)
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

        OnItemAdded?.Invoke(itemType);
    }

    /// <summary>
    /// Removes an item from the shopping center's repository.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public virtual void RemoveItem(System.Type itemType)
    {
        if (!ShoppingHelper.IsOfTypeItem(itemType))
        {
            Debug.LogWarning(gameObject.name + ": " + itemType + " is not a valid Item and cannot be removed from the shopping list.");
            return;
        }


        // If we have that item in the list, remove 1 from its quantity.
        if (shoppingDictionary.ContainsKey(itemType) && shoppingDictionary[itemType] > 0)
        {
            shoppingDictionary[itemType] -= 1;
        }      
        // We don't have that item in our list, so warn the user.
        else
        {
            Debug.LogWarning(gameObject.name + ": Cannot remove item of type " + itemType + " because it is not in our shopping list!\n" +
                "Either we don't have it or quantity is 0.");
            return;
        }

        Debug.Log(gameObject.name + ": Removed item " + itemType.ToString() + " to the list.\n" +
            "Quantity of item is now " + shoppingDictionary[itemType]);

        OnItemRemoved?.Invoke(itemType);

        // If that was the last item in the list, remove it.
        if (shoppingDictionary[itemType] <= 0)
        {
            Debug.Log("No more " + itemType.ToString());
            shoppingDictionary.Remove(itemType);
        }
    }

    /// <summary>
    /// Gets the quantity of an item type.
    /// </summary>
    /// <param name="itemType">The type of item to check the quantity of.</param>
    /// <returns>The quantity of the item type.</returns>
    public int GetQuantity(System.Type itemType)
    {
        if (shoppingDictionary.TryGetValue(itemType, out int quantity))
            return quantity;

        // Return the quantity if there is one, else return 0.
        return 0;
    }
}