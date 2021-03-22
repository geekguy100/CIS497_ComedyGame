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
    public static event Action OnGameStart;
    public static event Action OnShoppingCenterFilled;
    public static event Action OnGameWin;
    public static event Action OnGameLost;
    public static event Action OnNPCLeaveStore;


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

    /// <summary>
    /// Signals any observers that the game has started,
    /// </summary>
    public static void GameStart()
    {
        OnGameStart?.Invoke();
        OnShoppingCenterFilled?.Invoke();
    }

    /// <summary>
    /// Invoked when the player successfully checks out.
    /// </summary>
    public static void GameWin()
    {
        OnGameWin?.Invoke();
    }

    /// <summary>
    /// Invoked when the player can no longer complete their shopping list.
    /// </summary>
    public static void GameLost()
    {
        OnGameLost?.Invoke();
    }

    /// <summary>
    /// Invoked when an NPC leaves the store.
    /// </summary>
    public static void NPCLeaveStore()
    {
        OnNPCLeaveStore?.Invoke();
    }
}
