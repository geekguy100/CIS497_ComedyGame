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

    private void OnEnable()
    {
        EventManager.OnItemSpawned += AddItem;
        EventManager.OnItemTaken += RemoveItem;

        // TODO: Items should only be removed from the ShoppingCenter if an NPC checks out.
        // If an NPC doesn't check out and has an item on them, then it's technically still in the shopping center.
        // The player should be aware of this.
    }

    private void OnDisable()
    {
        EventManager.OnItemSpawned -= AddItem;
        EventManager.OnItemTaken -= RemoveItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventManager.ItemSpawned(typeof(Orange));
        }
    }
}
