/*****************************************************************************
// File Name :         ScoreManager.cs
// Author :            Kyle Grenier
// Creation Date :     03/06/2021
//
// Brief Description : Manages comparing the player's shopping list to what's left in the store (win/loss conditions).
*****************************************************************************/
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private CharacterShoppingList playerShoppingList;
    private CharacterInventory playerInventory;

    private bool gameOver = false;


    private void Awake()
    {
        // Initialize the player's shopping list and inventory.
        playerShoppingList = GameObject.FindGameObjectWithTag("PlayerShoppingList").GetComponent<CharacterShoppingList>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventory>();
    }

    private void OnEnable()
    {
        EventManager.OnNPCLeaveStore += CheckGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnNPCLeaveStore -= CheckGameOver;
    }

    // ---OLD GAME OVER CHECKING---
    //private void OnEnable()
    //{
    //    EventManager.OnItemTaken += CompareList;
    //    playerInventory.OnItemAdded += CompareList;
    //}

    //private void OnDisable()
    //{
    //    EventManager.OnItemTaken -= CompareList;
    //    playerInventory.OnItemAdded -= CompareList;
    //}

    /// <summary>
    /// Compares the player's shopping list with the quantity of an item type
    /// left in the shopping center. If the player can no longer obtain the amount of items from the shopping center,
    /// an arrow will display above NPCs with that item in their inventory.
    /// </summary>
    /// <param name="itemType">The type of item to check for.</param>
    void CompareList(System.Type itemType)
    {
        if (gameOver)
            return;

        // If there are no more items of this type available to pick up in the store, point to the nearest NPC with this type of item on them.
        // If no NPC has any, GAME OVER.
        if (ShoppingCenter.instance.GetQuantity(itemType) < (playerShoppingList.GetQuantity(itemType) - playerInventory.GetQuantity(itemType)))
        {
            GameObject[] npcsWithItem = ShoppingHelper.GetNPCsWithItemType(itemType);

            // We couldn't find any NPCs with the required item, so it's GAME OVER.
            if (npcsWithItem == null || npcsWithItem.Length == 0)
            {
                gameOver = true;
                EventManager.GameLost();
                return;
            }

            // For each NPC with the item we need, set its arrow active.
            foreach (GameObject npc in npcsWithItem)
            {
                // The NPC's arrow is not active, so we'll activate it.
                if (!npc.transform.GetChild(1).gameObject.activeInHierarchy)
                    npc.transform.GetChild(1).gameObject.SetActive(true);
            }

            // Deactivate the arrow if it is active and the NPC does not have that item on them.
            foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
            {
                CharacterInventory npcInventory = npc.GetComponent<CharacterInventory>();
                if (npcInventory.GetQuantity(itemType) <= 0 && npc.transform.GetChild(1).gameObject.activeInHierarchy)
                    npc.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// Checks to see if the game is over,
    /// meaning the player can no longer complete their shopping list.
    /// </summary>
    public void CheckGameOver()
    {
        /*
        Compare player shopping list quantities with the quantity of items in the store.
        playerShoppingList - playerInventory = remainder of items they need
        if the store's quantity  < remainder && no NPCs have enough of that item, GAME OVER.
         */

        ItemContainerData[] playerShoppingListData = playerShoppingList.GetItemData();
        foreach (ItemContainerData itemData in playerShoppingListData)
        {
            System.Type itemType = System.Type.GetType(itemData.ItemType);
            //int howManyMoreOfThisItemWeNeed = itemData.Quantity - playerInventory.GetQuantity(itemType);
            int howManyMoreOfThisItemWeNeed = ShoppingHelper.PlayerNeeds(itemType);
            int storeQuantity = ShoppingCenter.instance.GetQuantity(itemType);

            bool foundQuantity = false;

            // If this is true, then we cannot rely on the store for
            // obtaining the item we need, so let's look for any NPCs that have enough
            // of the item we need.
            if (storeQuantity < howManyMoreOfThisItemWeNeed)
            {
                Debug.LogWarning("Item Type: " + itemType.ToString() + " --- " + 
                    "Player Needs: " + howManyMoreOfThisItemWeNeed + " --- " + 
                    "Shopping Center Quantity: " + ShoppingCenter.instance.GetQuantity(itemType) + " --- " +
                    "NPC Total Quantity: " + ShoppingHelper.GetTotalNPCQuantity(itemType));


                //GameObject[] npcsWithItem = ShoppingHelper.GetNPCsWithItemType(itemType);
                //foreach (GameObject npc in npcsWithItem)
                //{
                //    CharacterShoppingList npcShoppingList = npc.GetComponent<CharacterShoppingList>();
                //    if (npcShoppingList.GetQuantity(itemType) >= howManyMoreOfThisItemWeNeed)
                //    {
                //        // We have enough of this item left to search for, 
                //        // so we'll break out of the loop and continue on to the next item.
                //        foundQuantity = true;
                //        break;
                //    }
                //}

                // If we can get enough items from the NPCs, continue the game.
                if (ShoppingHelper.GetTotalNPCQuantity(itemType) >= howManyMoreOfThisItemWeNeed)
                {
                    foundQuantity = true;
                }


                // If foundQuantity is false, it means that the shopping center
                // did not have enough items AND there were no NPCs containing the item we
                // needed, so it is GAME OVER.
                if (!foundQuantity)
                {
                    EventManager.GameLost();
                    break;
                }
            }
        }
    }
}
