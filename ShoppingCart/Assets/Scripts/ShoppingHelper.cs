/*****************************************************************************
// File Name :         ShoppingHelper.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : Static class to help manage backend checks related to shopping.
*****************************************************************************/
using UnityEngine;

public static class ShoppingHelper
{
    /// <summary>
    /// Checks to see if the type is that of an Item.
    /// </summary>
    /// <param name="t">The type in question.</param>
    /// <returns>True if the type is that of an Item.</returns>
    public static bool IsOfTypeItem(System.Type t)
    {
        return t.BaseType == typeof(Item);
    }
}
