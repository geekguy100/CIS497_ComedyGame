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

    [Tooltip("The visual prefab of this item container.")]
    [SerializeField] private GameObject visualPrefab;

    private GameObject visual;

    void Start()
    {
        // Make sure this item container has a valid type associated with it.
        if (!ShoppingHelper.IsOfTypeItem(System.Type.GetType(itemType)))
        {
            Debug.LogWarning(gameObject.name + " cannot hold the non-Item type of " + itemType);
            Destroy(this);
            return;
        }

        // Make sure the event manager knows the number of items in this cart.
        for (int i = 0; i < quantity; ++i)
        {
            EventManager.ItemSpawned(System.Type.GetType(itemType));
        }

        visual = Instantiate(visualPrefab, transform.position, visualPrefab.transform.rotation);
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
        else if (quantity <= 0 && visual != null)
            Destroy(visual);
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