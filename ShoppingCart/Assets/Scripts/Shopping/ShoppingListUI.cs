/*****************************************************************************
// File Name :         ShoppingListUI.cs
// Author :            Kyle Grenier
// Creation Date :     02/24/2021
//
// Brief Description : Displays a shopping list on a UI component and compares 
                       the quantites of items on the list to those in the shopping center.
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
        quantity = 1;
    }

    public System.Type itemType;
    public int quantity;
    public TextMeshProUGUI textComponent;
}

public class ShoppingListUI : MonoBehaviour
{
    [Tooltip("The inventory to display.")]
    [SerializeField] private ShoppingList shoppingList;
    private List<ListItem> itemsOnList;

    private void Awake()
    {
        itemsOnList = new List<ListItem>();
    }

    private void OnEnable()
    {
        if (shoppingList == null)
        {
            Debug.LogWarning(gameObject.name + " has no shopping list to display...");
            Destroy(gameObject);
            return;
        }

        shoppingList.OnItemAdded += AddToList;
        shoppingList.OnItemRemoved += RemoveFromList;
    }

    private void OnDisable()
    {
        if (shoppingList == null)
            return;

        shoppingList.OnItemAdded -= AddToList;
        shoppingList.OnItemRemoved -= RemoveFromList;
    }

    /// <summary>
    /// Adds an item of type itemType to the list or updates the quantity if it is already
    /// on the list.
    /// </summary>
    /// <param name="itemType">The item type to add.</param>
    private void AddToList(System.Type itemType)
    {
        Debug.Log("Trying to add item type " + itemType.ToString() + " to the list.");
        foreach (ListItem item in itemsOnList)
        {
            if (item.itemType == itemType)
            {
                ++item.quantity;
                SetText(item);
                return;
            }
        }

        // Duplicate the existing text -- we'll change it later!
        GameObject textHolder = Instantiate(transform.GetChild(0).gameObject, transform);
        ListItem newItem = new ListItem(itemType, textHolder.GetComponent<TextMeshProUGUI>());
        SetText(newItem);

        itemsOnList.Add(newItem);
    }

    /// <summary>
    /// Decreases the quantity of an item on the shopping list UI.
    /// </summary>
    /// <param name="itemType"></param>
    private void RemoveFromList(System.Type itemType)
    {
        foreach (ListItem item in itemsOnList)
        {
            if (item.itemType == itemType)
            {
                --item.quantity;

                // Item quantity is 0, so take it off the list.
                if (item.quantity <= 0)
                {
                    itemsOnList.Remove(item);
                    Destroy(item.textComponent.gameObject);
                    return;
                }

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
        item.textComponent.text = item.itemType.ToString() + ": " + item.quantity + " / " + ShoppingCenter.instance.GetQuantity(item.itemType);
    }

    private void UpdateAllText()
    {
        foreach(ListItem item in itemsOnList)
        {
            SetText(item);
        }
    }
}
