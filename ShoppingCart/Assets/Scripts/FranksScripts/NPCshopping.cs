using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCshopping : NPCBehavior
{
    // The NavMeshAgent.
    private NavMeshAgent agent;

    // The NPCs inventory.
    private CharacterInventory inventory;

    // The NPCs shopping list.
    private CharacterShoppingList characterShoppingList;

    // Data about the NPCs current shopping behavior.
    private NPCShoppingData shoppingData;

    // The destiation of the item.
    private ItemContainer destination;

    // The data of the item we are searching for.
    private ItemContainerData itemData;

    private bool beganItemCollection = false;

    // The parent NPC.
    private NPC npc;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<CharacterInventory>();
        characterShoppingList = GetComponent<CharacterShoppingList>();
        shoppingData = GetComponent<NPCShoppingData>();
    }


    /// <summary>
    /// Perform setup.
    /// </summary>
    private void Initialize()
    {
        // If the NPC is done shopping, don't run.
        if (shoppingData.DoneShopping)
            return;

        print("~~~~INITIALIZING~~~~");

        // NPC has obtained all items and needs to check out.
        if (shoppingData.Index >= characterShoppingList.GetItemData().Length)
        {
            Debug.LogWarning(gameObject.name + " is done collecting his items. He needs to check out...");
            shoppingData.DoneShopping = true;
            npc.myState = NPC.State.Checkout;
            //agent.SetDestination(ShoppingHelper.GetNearestCheckoutLocation(transform).transform.position);
            return;
        }
        else if (shoppingData.Index < 0)
        {
            Debug.LogError(gameObject.name + " WHY IS MY INDEX < 0??");
            return;
        }

        itemData = characterShoppingList.GetItemData()[shoppingData.Index];
        Transform nearestContainer = ShoppingHelper.GetNearestContainerOfType(transform, System.Type.GetType(itemData.ItemType));

        // If we don't have a destination, find the nearest containter of the needed item type and path to it.
        if (nearestContainer != null)
        {
            destination = ShoppingHelper.GetNearestContainerOfType(transform, System.Type.GetType(itemData.ItemType)).GetComponent<ItemContainer>();
            agent.SetDestination(destination.transform.position);
        }
        // If there are no items in stock of this type, then leave the store.
        else
        {
            Debug.LogWarning(gameObject.name + ": No items of type " + itemData.ItemType + " left in the store... NPC is done shopping.");
            shoppingData.DoneShopping = true;
            agent.SetDestination(GameObject.FindGameObjectWithTag("Finish").transform.position);
        }
    }

    public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, ItemContainerData[] shoppingListData, CharacterInventory inventory, NPC.State myState)
    {
        if (this.npc == null)
            this.npc = npc;

        if (shoppingData.DoneShopping)
            return;

        // Initialization
        if (destination == null)
        {
            print("DESTINATION NULL. INITIALIZING.");
            Initialize();
            return;
        }

        // If the NPC is in range of the item, perform collection sequence.
        if (Vector3.Distance(transform.position, destination.transform.position) < 4f)
        {
            if (!beganItemCollection)
            {
                beganItemCollection = true;
                StartCoroutine(CollectItems());
            }

        }
    }

    /// <summary>
    /// NPC collects the items from the nearest ItemContainer.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CollectItems()
    {
        Debug.LogWarning(gameObject.name + ": COLLECTING: " + itemData.ItemType + " " + itemData.Quantity);
        for (int i = 0; i < itemData.Quantity; ++i)
        {
            // Make sure that our destination is still active.
            // If it's not, we'll have to find another item container.
            if (destination != null)
            {
                destination.Interact(inventory);
            }

            // If our destinarion is null (quantity ran out mid collection),
            // re-initialize this behaviour.
            else
            {
                Debug.LogWarning(gameObject.name + ": ITEM RAN OUT. " + itemData.ItemType);
                beganItemCollection = false;
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        Debug.LogWarning(gameObject.name + ": DONE COLLECTING " + itemData.ItemType);
        FinishCollection();
    }

    /// <summary>
    /// Invoked when the NPC finishes collecting items;
    /// Resets beganItemCollection to false and increases the shoppingData.Index property
    /// so the NPC knows to go to the next item.
    /// </summary>
    private void FinishCollection()
    {
        beganItemCollection = false;
        ++shoppingData.Index;
        Initialize();
    }


    //public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, ItemContainerData[] shoppingListData, CharacterInventory inventory, NPC.State myState)
    //{
    //    if (shoppingListData.Length <= 0 || listIndex > shoppingListData.Length - 1)
    //    {
    //        print("DONE WITH LIST!");
    //        return;
    //    }

    //    if (collecting && nearestContainer == null)
    //    {
    //        Debug.LogWarning("Collecting and nearest container is null!");

    //        if (lastCall != null)
    //        {
    //            StopCoroutine(lastCall);
    //            lastCall = null;
    //        }
            
    //        collecting = false;
    //        collectionComplete = false;
    //        npc.hasDestination = false;
    //        return;
    //    }


    //    //reset cart if it tips or turns weird (currently not working)
    //    if (Mathf.Abs(cart.transform.localRotation.z) >= 25 || Mathf.Abs(cart.transform.localRotation.y) >= 45)
    //    {
    //        Debug.Log("My cart tipped over!");
    //        cart.transform.localRotation = cartLocalRot;
    //        Debug.Log("Adjusted my cart");
    //    }

    //    //if you don't have a destination, set one and begin walking there
    //    if (hasDestination == false)
    //    {
    //        itemType = System.Type.GetType(shoppingListData[listIndex].ItemType);
    //        nearestContainer = ShoppingHelper.GetNearestContainerOfType(transform, itemType).GetComponent<ItemContainer>();

    //        agent.SetDestination(nearestContainer.transform.position);
    //        npc.hasDestination = true;
    //    }

    //    //if (nearestContainer == null)
    //    //{
    //    //    //nearestContainer = ShoppingHelper.GetNearestContainerOfType(transform, itemType).GetComponent<ItemContainer>();
    //    //    npc.hasDestination = false;

    //    //    itemType = null;
    //    //    nearestContainer = null;
    //    //    collectionComplete = false;

    //    //    return;
    //    //}

    //    //if you're at your destination, say you no longer have a destination, and choose your next one
    //    if (Vector3.Distance(transform.position, nearestContainer.transform.position) <= 4)
    //    {
    //        // Interact with the item container to add the right amount of items to our inventory.
    //        if (!collectionComplete && !collecting)
    //            lastCall = StartCoroutine(CollectItems(shoppingListData[listIndex], inventory));

    //        if (collectionComplete)
    //        {
    //            print("DONE COLLECTING " + shoppingListData[listIndex].ItemType);
    //            // Seek out the next item on the list.
    //            npc.listIndex++;

    //            itemType = null;
    //            nearestContainer = null;
    //            collectionComplete = false;

    //            npc.hasDestination = false;
    //            return;
    //        }
    //    }
    //}

    //private bool collecting = false;
    //private bool collectionComplete = false;
    ////private IEnumerator CollectItems(ItemContainerData itemToGet, CharacterInventory inventory)
    ////{
    ////    collecting = true;
    ////    for (int i = 0; i < itemToGet.Quantity; ++i)
    ////    {
    ////        if (nearestContainer == null)
    ////        {
    ////            Debug.LogWarning("Nearest container destoyed mid pickup...");
    ////            yield break;
    ////        }

    ////        nearestContainer.Interact(inventory);
    ////        yield return new WaitForSeconds(0.5f);
    ////    }
    ////    collecting = false;
    ////    collectionComplete = true;
    ////}
}
