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

    /// <summary>
    /// Gets an NPC that has a type of item in their inventory.
    /// </summary>
    /// <param name="itemType">The type of item </param>
    /// <returns>An NPC that has an item of the given type.</returns>
    public static GameObject[] GetNPCsWithItemType(System.Type itemType)
    {
        // Invalid item type. Return null.
        if (!IsOfTypeItem(itemType))
            return null;

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        // There are no NPCs left in the store, so return null!
        if (npcs.Length <= 0)
            return null;

        return npcs
            .Where(t => t.GetComponent<CharacterInventory>().GetQuantity(itemType) > 0 && !t.GetComponent<NPCShoppingData>().CheckingOut)
            .ToArray();
    }

    /// <summary>
    /// Returns true if an NPC has enough of an item the player needs.
    /// </summary>
    /// <param name="npcInventory">The NPC's inventory.</param>
    /// <param name="playerInventory">the player's inventory.</param>
    /// <param name="playerShoppingList">The player's shopping list.</param>
    /// <param name="itemType">The type of item to check for.</param>
    /// <returns></returns>
    public static bool NPCHasEnoughOfItem(CharacterInventory npcInventory, CharacterInventory playerInventory, CharacterShoppingList playerShoppingList, System.Type itemType)
    {
        if (!IsOfTypeItem(itemType))
            return false;

        int npcItemQuantity = npcInventory.GetQuantity(itemType);
        int quantityPlayerNeeds = playerShoppingList.GetQuantity(itemType) - playerInventory.GetQuantity(itemType);

        // Does the NPC has the remainder of items the player needs?
        if (npcItemQuantity >= quantityPlayerNeeds)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns the summed quantity of an item from all the NPC's inventories.
    /// </summary>
    /// <param name="itemType">The type of item to check for.</param>
    /// <returns>The summed quantity of an item from all the NPC's inventories.</returns>
    public static int GetTotalNPCQuantity(System.Type itemType)
    {
        if (!IsOfTypeItem(itemType))
            return -1;

        int totalQuantity = 0;
        GameObject[] npcsWithItem = GetNPCsWithItemType(itemType);
        foreach (GameObject npc in npcsWithItem)
        {
            CharacterInventory npcInventory = npc.GetComponent<CharacterInventory>();
            totalQuantity += npcInventory.GetQuantity(itemType);
        }

        return totalQuantity;
    }

    /// <summary>
    /// Returns how much more of a particular item the player needs.
    /// </summary>
    /// <param name="itemType">The type of item to check for.</param>
    /// <returns>How much more of the item the player needs.</returns>
    public static int PlayerNeeds(System.Type itemType)
    {
        if (!IsOfTypeItem(itemType))
            return -1;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterInventory playerInventory = player.GetComponent<CharacterInventory>();
        CharacterShoppingList playerShoppingList = player.GetComponentInChildren<CharacterShoppingList>();

        return playerShoppingList.GetQuantity(itemType) - playerInventory.GetQuantity(itemType);
    }
}
