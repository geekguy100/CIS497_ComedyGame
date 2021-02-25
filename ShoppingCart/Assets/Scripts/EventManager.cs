/*****************************************************************************
// File Name :         EventManager.cs
// Author :            Kyle Grenier
// Creation Date :     02/21/2021
//
// Brief Description : Script to manage events - Observer Pattern.
*****************************************************************************/
using System;

public static class EventManager
{
    public static event Action<System.Type> OnItemTaken;
    public static event Action<System.Type> OnItemSpawned;


    /// <summary>
    /// Invoked when an item is taken from the shopping center.
    /// </summary>
    public static void ItemTaken(System.Type item)
    {
        OnItemTaken?.Invoke(item);
    }

    /// <summary>
    /// Invoked when an item is spawned into the scene.
    /// </summary>
    /// <param name="itemType">The type of item that was spawned.</param>
    public static void ItemSpawned(System.Type itemType)
    {
        if (!ShoppingHelper.IsOfTypeItem(itemType))
            return;

        OnItemSpawned?.Invoke(itemType);
    }
}
