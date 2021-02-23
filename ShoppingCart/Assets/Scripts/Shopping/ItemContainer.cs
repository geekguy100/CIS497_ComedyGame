/*****************************************************************************
// File Name :         ItemContainer.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : Class for defining functionality of an item container.
*****************************************************************************/
using UnityEngine;

public struct ItemContainerData
{
    public ItemContainerData(string itemType, int quantity)
    {
        this.itemType = itemType;
        this.quantity = quantity;
    }

    string itemType;
    int quantity;
}

public class ItemContainer : MonoBehaviour, IInteractable
{
    [Tooltip("The item type that this container holds")]
    [SerializeField] private string itemType;

    [Tooltip("The number of items present in this container.")]
    [SerializeField] private int quantity;

    void Start()
    {
        // Make sure this item container has a valid type associated with it.
        if (!ShoppingHelper.IsOfTypeItem(System.Type.GetType(itemType)))
        {
            Debug.LogWarning(gameObject.name + " cannot hold the non-Item type of " + itemType);
            Destroy(this);
            return;
        }
    }

    /// <summary>
    /// Iteract with the item container to take an item from it.
    /// </summary>
    /// <param name="sender">The GameObject interacting with the item container.</param>
    public void Interact(GameObject sender)
    {
        ShoppingList senderList = sender.GetComponent<ShoppingList>();

        if (senderList != null && quantity > 0)
        {
            --quantity;
            Debug.Log(gameObject.name + " ItemContainer quantity of " + itemType + " now at " + quantity);
            senderList.AddItem(System.Type.GetType(itemType));
        }
    }

    /// <summary>
    /// Gets the type of item and its quantity from the ItemContainer.
    /// </summary>
    /// <returns>The ItemContainerData containing the ItemContainer's item type and quantity of that item.</returns>
    public ItemContainerData GetData()
    {
        return new ItemContainerData(itemType, quantity);
    }
}