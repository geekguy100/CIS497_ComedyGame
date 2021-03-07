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
        EventManager.OnItemTaken += CompareList;
        playerInventory.OnItemAdded += CompareList;
    }

    private void OnDisable()
    {
        EventManager.OnItemTaken -= CompareList;
        playerInventory.OnItemAdded -= CompareList;
    }

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
            if (npcsWithItem == null || npcsWithItem.Length <= 0)
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
}
