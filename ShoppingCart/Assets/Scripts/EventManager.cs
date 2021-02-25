/*****************************************************************************
// File Name :         EventManager.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
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

    public static void ItemSpawned(System.Type item)
    {
        OnItemSpawned?.Invoke(item);
    }
}
