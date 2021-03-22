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
    }

    public System.Type itemType;
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
        if (inventory == null || shoppingList == null)
        {
            Debug.LogWarning(gameObject.name + " has no inventory or shopping list to display...");
            Destroy(gameObject);
            return;
        }

        // If an item is added to or removed from our inventory, update the UI.
        inventory.OnItemAdded += UpdateList;
        inventory.OnItemRemoved += UpdateList;

        // If an item is added or removed from our shopping list (the items we need), make sure
        // to update the UI.
        shoppingList.OnItemAdded += UpdateList;
        shoppingList.OnItemRemoved += UpdateList;

        // If an item is taken from the store, make sure to update the UI bc we
        // take the store's quantity into consideration!
        EventManager.OnItemTaken += UpdateList;
        EventManager.OnItemSpawned += UpdateList;
    }

    private void OnDisable()
    {
        if (inventory == null)
            return;

        inventory.OnItemAdded -= UpdateList;
        inventory.OnItemRemoved -= UpdateList;
        shoppingList.OnItemAdded -= UpdateList;
        shoppingList.OnItemRemoved -= UpdateList;
        EventManager.OnItemTaken -= UpdateList;
        EventManager.OnItemSpawned -= UpdateList;
    }

    /// <summary>
    /// Updates the text of an item on the UI list.
    /// </summary>
    /// <param name="itemType">The type of item to update the text of.</param>
    private void UpdateList(System.Type itemType)
    {
        // Prevent misc items from being added to shopping list.
        // Misc items will still be added to the character's inventory however, just not updated
        // on the shopping list UI.
        if (!shoppingList.ContainsType(itemType))
            return;

        foreach(ListItem item in itemsOnList)
        {
            // If the item is already on our UI, update the text.
            if(item.itemType == itemType)
            {
                UpdateAllText();
                return;
            }
        }

        // If the item is not on our UI, create a new text item.
        // Duplicate the existing text -- we'll change it later!
        GameObject textHolder = Instantiate(transform.GetChild(0).gameObject, transform);
        ListItem newItem = new ListItem(itemType, textHolder.GetComponent<TextMeshProUGUI>());
        SetText(newItem);

        itemsOnList.Add(newItem);
    }

    /// <summary>
    /// Updates the text of a list item.
    /// </summary>
    /// <param name="item">The ListItem to update.</param>
    private void SetText(ListItem item)
    {
        Debug.Log(ShoppingCenter.instance.GetQuantity(item.itemType));
        // The amount the character has, the amount they need, and the amount in the store.
        item.textComponent.text = 
            item.itemType.ToString() + ": " + 
            inventory.GetQuantity(item.itemType) + " / " + 
            shoppingList.GetQuantity(item.itemType) + " / " + 
            ShoppingCenter.instance.GetQuantity(item.itemType);

        // If we have enough of the item we need, make the text color green.
        if (inventory.GetQuantity(item.itemType) >= shoppingList.GetQuantity(item.itemType))
            item.textComponent.color = Color.green;

        // If there are no longer enough items of this type present in the shopping center, make the text color red.
        else if (ShoppingCenter.instance.GetQuantity(item.itemType) < shoppingList.GetQuantity(item.itemType) - inventory.GetQuantity(item.itemType))
            item.textComponent.color = Color.red;

        // If we're still collecting this type of item and there are enough present in the store, make the text color black.
        else
            item.textComponent.color = Color.black;
    }

    /// <summary>
    /// Updates all text on the list.
    /// </summary>
    /// <param name="itemType">Not used.</param>
    private void UpdateAllText()
    {
        foreach(ListItem item in itemsOnList)
        {
            SetText(item);
        }
    }
}