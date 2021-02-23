/*****************************************************************************
// File Name :         ShoppingCenter.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : The shopping list that contains all of the items 
                       and their quantities currently up for grabs in the store.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;

public sealed class ShoppingCenter : ShoppingList
{
    private void OnEnable()
    {
        EventManager.OnItemSpawned += AddItem;
        EventManager.OnItemTaken += RemoveItem;
    }

    private void OnDisable()
    {
        EventManager.OnItemSpawned -= AddItem;
        EventManager.OnItemTaken -= RemoveItem;
    }
}
