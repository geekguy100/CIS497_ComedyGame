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

    public static void ItemTaken(System.Type item)
    {
        OnItemTaken?.Invoke(item);
    }

    /// <summary>
    /// Invoked when an item is spawned into the scene.
    /// </summary>
    /// <param name="item"></param>
    public static void ItemSpawned(System.Type item)
    {
        if (!ShoppingHelper.IsOfTypeItem(item))
            return;

        OnItemSpawned?.Invoke(item);
    }
}
