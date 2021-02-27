/*****************************************************************************
// File Name :         Checkout.cs
// Author :            Kyle Grenier
// Creation Date :     02/27/2021
//
// Brief Description : Checks to see if the character has all of the items they need.
*****************************************************************************/
using UnityEngine;

public class Checkout : MonoBehaviour
{
    [SerializeField] private GameObject winText;

    /// <summary>
    /// Checks to see if the character has all of the items they need;
    /// compares the items in the inventory to the items on their shopping list.
    /// </summary>
    /// <param name="inventory">The character's inventory.</param>
    /// <param name="shoppingList">The character's shopping list.</param>
    public void Interact(CharacterInventory inventory, ShoppingList shoppingList)
    {
        // True if the character can check out.
        bool canCheckOut = true;

        ItemContainerData[] shoppingData = shoppingList.GetItemData();

        foreach(ItemContainerData item in shoppingData)
        {
            // If the item in the character's inventory matches in type and quantity of what is needed on the shopping list, 
            // keep iterating through the shopping list, checking for a mismatch.
            if (inventory.ContainsType(System.Type.GetType(item.ItemType)) && inventory.GetQuantity(System.Type.GetType(item.ItemType)) >= item.Quantity)
                continue;

            canCheckOut = false;
            break;
        }

        if (canCheckOut)
        {
            // Right now we're assuming only the player can check out.
            winText.SetActive(true);
        }
        else
            Debug.Log("Can't check out yet...");
    }
}
