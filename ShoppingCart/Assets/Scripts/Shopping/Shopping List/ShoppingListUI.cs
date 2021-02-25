/*****************************************************************************
// File Name :         ShoppingListUI.cs
// Author :            Kyle Grenier
// Creation Date :     02/24/2021
//
// Brief Description : Displays an inventory on a UI component, compares 
                       the quantites of items in the inventory to the number to obtain to the
                       quantity in the shopping center.
*****************************************************************************/
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ListItem
{
    public ListItem(System.Type itemType, TextMeshProUGUI textComponent)
    {
        this.itemType = itemType;
        this.textComponent = textComponent;
        inventoryQuantity = 0;
        shoppingListQuantity = 0;
    }

    public System.Type itemType;
    public int inventoryQuantity;
    public int shoppingListQuantity;
    public TextMeshProUGUI textComponent;
}

public sealed class ShoppingListUI : MonoBehaviour
{
    [Tooltip("The inventory to display.")]
    [SerializeField] private CharacterInventory inventory;

    [Tooltip("The character's shopping list. i.e. the items they need to obtain.")]
    [SerializeField] private CharacterShoppingList shoppingList;
    private List<ListItem> itemsOnList;

    private void Awake()
    {
        itemsOnList = new List<ListItem>();
    }

    private void OnEnable()
    {
        if (inventory == null)
        {
            Debug.LogWarning(gameObject.name + " has no inventory to display...");
            Destroy(gameObject);
            return;
        }

        // If an item is added to or removed from our inventory, update the UI.
        inventory.OnItemAdded += AddFromInventory;
        inventory.OnItemRemoved += RemoveFromInventory;

        // If an item is added or removed from our shopping list (the items we need), make sure
        // to update the UI.
        shoppingList.OnItemAdded += AddFromShoppingList;
        shoppingList.OnItemRemoved += RemoveFromShoppingList;

        // If an item is taken from the store, make sure to update the UI bc we
        // take the store's quantity into consideration!
        EventManager.OnItemTaken += UpdateAllText;
    }

    private void OnDisable()
    {
        if (inventory == null)
            return;

        inventory.OnItemAdded -= AddFromInventory;
        inventory.OnItemRemoved -= RemoveFromInventory;
        shoppingList.OnItemAdded -= AddFromShoppingList;
        shoppingList.OnItemRemoved -= RemoveFromShoppingList;
        EventManager.OnItemTaken -= UpdateAllText;
    }

    /// <summary>
    /// Adds an item of type itemType to the list or updates the quantity if it is already
    /// on the list.
    /// </summary>
    /// <param name="itemType">The item type to add.</param>
    private void AddFromInventory(System.Type itemType)
    {
        Debug.Log("Trying to add item type " + itemType.ToString() + " to the list.");
        foreach (ListItem item in itemsOnList)
        {
            if (item.itemType == itemType)
            {
                ++item.inventoryQuantity;
                SetText(item);
                return;
            }
        }

        // Duplicate the existing text -- we'll change it later!
        GameObject textHolder = Instantiate(transform.GetChild(0).gameObject, transform);
        ListItem newItem = new ListItem(itemType, textHolder.GetComponent<TextMeshProUGUI>());
        ++newItem.inventoryQuantity;
        SetText(newItem);

        itemsOnList.Add(newItem);
    }

    private void AddFromShoppingList(System.Type itemType)
    {
        Debug.Log("Trying to add item type " + itemType.ToString() + " to the list.");
        foreach (ListItem item in itemsOnList)
        {
            if (item.itemType == itemType)
            {
                ++item.shoppingListQuantity;
                SetText(item);
                return;
            }
        }

        // Duplicate the existing text -- we'll change it later!
        GameObject textHolder = Instantiate(transform.GetChild(0).gameObject, transform);
        ListItem newItem = new ListItem(itemType, textHolder.GetComponent<TextMeshProUGUI>());
        ++newItem.shoppingListQuantity;
        SetText(newItem);

        itemsOnList.Add(newItem);
    }

    /// <summary>
    /// Decreases the quantity of an item on the shopping list UI.
    /// </summary>
    /// <param name="itemType">The type of item to remove.</param>
    private void RemoveFromInventory(System.Type itemType)
    {
        foreach (ListItem item in itemsOnList)
        {
            if (item.itemType == itemType)
            {
                --item.inventoryQuantity;

                #region --- Remove the item off the list completely if it's not in our inventory ---
                //// Item quantity is 0, so take it off the list.
                //if (item.quantity <= 0)
                //{
                //    itemsOnList.Remove(item);
                //    Destroy(item.textComponent.gameObject);
                //    return;
                //}
                #endregion

                SetText(item);
                return;
            }
        }

        Debug.Log(gameObject.name + ": Shopping List UI could not remove item type " + itemType.ToString() + 
            " bc it could not be found on this list...");
    }

    /// <summary>
    /// Decreases the quantity of an item on the shopping list UI.
    /// </summary>
    /// <param name="itemType">The type of item to remove.</param>
    private void RemoveFromShoppingList(System.Type itemType)
    {
        foreach (ListItem item in itemsOnList)
        {
            if (item.itemType == itemType)
            {
                --item.shoppingListQuantity;

                #region --- Remove the item off the list completely if it's not in our inventory ---
                //// Item quantity is 0, so take it off the list.
                //if (item.quantity <= 0)
                //{
                //    itemsOnList.Remove(item);
                //    Destroy(item.textComponent.gameObject);
                //    return;
                //}
                #endregion

                SetText(item);
                return;
            }
        }

        Debug.Log(gameObject.name + ": Shopping List UI could not remove item type " + itemType.ToString() +
            " bc it could not be found on this list...");
    }

    /// <summary>
    /// Updates the text of a list item.
    /// </summary>
    /// <param name="item">The ListItem to update.</param>
    private void SetText(ListItem item)
    {
        // The amount the character has, the amount they need, and the amount in the store.
        item.textComponent.text = item.itemType.ToString() + ": " + item.inventoryQuantity + " / " + item.shoppingListQuantity + " / " + ShoppingCenter.instance.GetQuantity(item.itemType);
    }

    /// <summary>
    /// Updates all text on the list.
    /// </summary>
    /// <param name="itemType">Not used.</param>
    private void UpdateAllText(System.Type itemType)
    {
        foreach(ListItem item in itemsOnList)
        {
            SetText(item);
        }
    }
}