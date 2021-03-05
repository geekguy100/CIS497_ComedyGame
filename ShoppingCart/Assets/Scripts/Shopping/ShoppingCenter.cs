/*****************************************************************************
// File Name :         ShoppingCenter.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : The shopping list that contains all of the items 
                       and their quantities currently up for grabs in the store.
*****************************************************************************/
using UnityEngine;
public sealed class ShoppingCenter : ShoppingList
{
    #region Singleton
    public static ShoppingCenter instance;

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    // TODO;
    // Keep track of the total amount of items that should be added. 
    // Once that quantity has been reached, invoke OnShoppingCenterFilled.

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