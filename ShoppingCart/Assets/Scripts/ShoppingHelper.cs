/*****************************************************************************
// File Name :         ShoppingHelper.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : Static class to help manage backend checks related to shopping.
*****************************************************************************/
using UnityEngine;
using System.Linq;

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

    /// <summary>
    /// Gets the nearest item container of a type to a character.
    /// </summary>
    /// <param name="character">The character to check the nearest item container to.</param>
    /// <param name="itemType">The type of item that the item container should contain.</param>
    /// <returns></returns>
    public static Transform GetNearestContainerOfType(Transform character, System.Type itemType)
    {
        if (!IsOfTypeItem(itemType))
            return null;

        ItemContainer[] itemContainers = GameObject.FindObjectsOfType<ItemContainer>();
        return itemContainers
            .Where(t => System.Type.GetType(t.GetData().ItemType) == itemType)
            .OrderBy(t => Vector3.Distance(character.transform.position, t.transform.position))
            .ToArray()[0].transform;
    }
}
