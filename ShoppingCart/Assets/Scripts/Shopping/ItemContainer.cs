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

    public string ItemType { get { return itemType; } }
    public int Quantity { get { return quantity; } }

    public override string ToString()
    {
        return itemType.ToString() + ": " + quantity;
    }
}

public class ItemContainer : MonoBehaviour, IItemInteractable
{
    [Tooltip("The item type that this container holds")]
    [SerializeField] private string itemType;

    // The minimum amount of items present in this item container.
    private int minQuantity = 1;

    // The maximum amount of items present in this item container.
    private int maxQuantity = 10;

    // The amount of items currently present in this item container.
    private int currentQuantity = 0;

    [Tooltip("The visual prefab of this item container.")]
    [SerializeField] private GameObject visualPrefab;

    // The instantiated visual prefab.
    private GameObject visual;

    #region --- Subscribing and Unsubscribing to EventManager events ---
    private void OnEnable()
    {
        EventManager.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= OnGameStart;
    }
    #endregion

    private void OnGameStart()
    {
        // Make sure this item container has a valid type associated with it.
        if (!ShoppingHelper.IsOfTypeItem(System.Type.GetType(itemType)))
        {
            Debug.LogWarning(gameObject.name + " cannot hold the non-Item type of " + itemType);
            Destroy(this);
            return;
        }

        // Calculate the quantity of items this container will hold.
        currentQuantity = Random.Range(minQuantity, maxQuantity + 1);

        // Make sure the event manager knows the number of items in this cart.
        for (int i = 0; i < currentQuantity; ++i)
        {
            EventManager.ItemSpawned(System.Type.GetType(itemType));
        }

        visual = Instantiate(visualPrefab, transform.position, visualPrefab.transform.rotation);
    }

    /// <summary>
    /// Iteract with the item container to take an item from it.
    /// </summary>
    /// <param name="sender">The GameObject interacting with the item container.</param>
    public void Interact(ShoppingList senderList)
    {
        //ShoppingList senderList = sender.GetComponent<ShoppingList>();

        if (senderList != null && currentQuantity > 0)
        {
            --currentQuantity;
            Debug.Log(gameObject.name + ": ItemContainer quantity of " + itemType + " now at " + currentQuantity);
            senderList.AddItem(System.Type.GetType(itemType));
            EventManager.ItemTaken(System.Type.GetType(itemType));
        }

        // Destroy the visual prefab so the players know they cannot access this item container.
        if (currentQuantity <= 0 && visual != null)
        {
            Destroy(visual);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Gets the type of item and its quantity from the ItemContainer.
    /// </summary>
    /// <returns>The ItemContainerData containing the ItemContainer's item type and quantity of that item.</returns>
    public ItemContainerData GetData()
    {
        return new ItemContainerData(itemType, currentQuantity);
    }
}