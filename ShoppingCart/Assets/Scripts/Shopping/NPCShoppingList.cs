/* Frank Calabrese
 * NPCShoppingList.cs
 * The NPC's inner most wants and desires at your fingertips
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShoppingList : ShoppingList
{
    [Tooltip("The maximum amount of items this shopping list can hold.")]
    [SerializeField] private int maxItems = 10;
    public ItemContainerData[] itemsINeed;

    private void OnEnable()
    {
        EventManager.OnShoppingCenterFilled += PopulateList;
    }

    private void OnDisable()
    {
        EventManager.OnShoppingCenterFilled -= PopulateList;
    }

    private void PopulateList()
    {
        int i = 0;
        while (GetTotalQuantity() < maxItems)
        {
            ItemContainerData randomItem = ShoppingCenter.instance.GetRandomItem();

            // If the random item we got is already on our list, get another one.
            if (ContainsType(System.Type.GetType(randomItem.ItemType)))
                continue;

            //int randomQuantity = Random.Range(1, randomItem.Quantity / 2 + 1);

            // There's a chance the random quantity will bring us over the max amount of items.
            // To prevent that, we'll decrease the randomQuantity as much as we need to.
            //while (randomQuantity + GetTotalQuantity() > maxItems)
            //{
                //--randomQuantity;
            //}

            //for (int i = 0; i < randomQuantity; ++i)
            AddItem(System.Type.GetType(randomItem.ItemType));
            itemsINeed[i] = randomItem;
            i++;
        }
    }

}
