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
    /// and lets observers know when the shopping center is filled.
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
}
