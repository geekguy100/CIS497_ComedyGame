/*****************************************************************************
// File Name :         CharacterShoppingList.cs
// Author :            Kyle Grenier
// Creation Date :     02/24/2021
//
// Brief Description : The list of goods the character needs to obtain.
*****************************************************************************/
using UnityEngine;
using System;

public class CharacterShoppingList : ShoppingList
{
    [Tooltip("The maximum amount of items this shopping list can hold.")]
    [SerializeField] private int maxItems = 20;

    public event Action OnListPopulated;

    private void OnEnable()
    {
        EventManager.OnShoppingCenterFilled += PopulateList;
    }

    private void OnDisable()
    {
        EventManager.OnShoppingCenterFilled -= PopulateList;
    }

    /// <summary>
    /// Populates the shopping list with a random set of items
    /// and random quantities.
    /// </summary>
    private void PopulateList()
    {
        while (GetTotalQuantity() < maxItems)
        {
            ItemContainerData randomItem = ShoppingCenter.instance.GetRandomItem();

            // If the random item we got is already on our list, get another one.
            if (ContainsType(System.Type.GetType(randomItem.ItemType)))
                continue;

            // Try to get at least 3 of an item.
            int min = UnityEngine.Random.Range(2, 5);
            int max = Mathf.FloorToInt(randomItem.Quantity / 2) + 1;
            int randomQuantity = UnityEngine.Random.Range(min, max);

            // If the randomQuantity calculated is greater than the quantity of that item in the store,
            // decrease randomQuantity until we're at a safe number.
            while (randomQuantity > randomItem.Quantity)
            {
                --randomQuantity;
            }

            // There's a chance the random quantity will bring us over the max amount of items.
            // To prevent that, we'll decrease the randomQuantity as much as we need to.
            while(randomQuantity + GetTotalQuantity() > maxItems)
            {
                --randomQuantity;
            }

            for (int i = 0; i < randomQuantity; ++i)
            {
                AddItem(System.Type.GetType(randomItem.ItemType));
            }
        }

        OnListPopulated?.Invoke();
    }
}
