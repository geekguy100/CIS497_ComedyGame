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
    /// <returns>The nearest item container of the given item type.</returns>
    public static Transform GetNearestContainerOfType(Transform character, System.Type itemType)
    {
        if (!IsOfTypeItem(itemType))
            return null;

        ItemContainer[] itemContainers = GameObject.FindObjectsOfType<ItemContainer>();

        // If there are no items of this type in the scene, return null.
        if (itemContainers.Length <= 0)
            return null;

        // If there are some items of this type in the scene, return the nearest to the character.
        ItemContainer container = itemContainers
            .Where(t => System.Type.GetType(t.GetData().ItemType) == itemType && t.gameObject.activeInHierarchy)
            .OrderBy(t => Vector3.Distance(character.transform.position, t.transform.position))
            .FirstOrDefault();

        // If the container found is null, return null.
        // Else, return the transform component of the container found.
        if (container == null)
            return null;
        else
            return container.transform;
    }

    /// <summary>
    /// Gets the nearest checkout game object from a character.
    /// </summary>
    /// <param name="character">The character to find the nearest checkout location to.</param>
    /// <returns>The nearest checkout location.</returns>
    public static GameObject GetNearestCheckoutLocation(Transform character)
    {
        GameObject[] checkoutLocations = GameObject.FindGameObjectsWithTag("CheckoutLocation");
        if (checkoutLocations.Length <= 0)
            return null;

        return checkoutLocations
            .OrderBy(t => Vector3.Distance(character.position, t.transform.position))
            .FirstOrDefault();
    }
}
