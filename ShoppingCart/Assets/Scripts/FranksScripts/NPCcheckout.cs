/*****************************************************************************
// File Name :         NPCcheckout.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : NPC behavior for checking out items -- this behavior is set when the NPC has collected all items.
*****************************************************************************/
using UnityEngine;
using UnityEngine.AI;

public class NPCcheckout : NPCBehavior
{
    // The NPCs inventory.
    private CharacterInventory inventory;

    // The NPCs shopping list.
    private CharacterShoppingList characterShoppingList;

    // The NPCs shopping data.
    private NPCShoppingData shoppingData;

    // The NPCs NavMeshAgent component.
    private NavMeshAgent agent;

    // The NPCs destination.
    private Transform destination;


    private void Awake()
    {
        inventory = GetComponent<CharacterInventory>();
        characterShoppingList = GetComponent<CharacterShoppingList>();
        shoppingData = GetComponent<NPCShoppingData>();

        agent = GetComponent<NavMeshAgent>();
    }

    public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, ItemContainerData[] shoppingListArray, CharacterInventory inventory, NPC.State myState)
    {
        // If the item ran out of stock, head towards the exit.
        if (shoppingData.OutOfStock && destination == null)
        {
            SFXManager.instance.source.PlayOneShot(SFXManager.instance.huh);
            Debug.Log(gameObject.name + ": item needed it out of stock, so he'll head to the exit.");
            destination = GameObject.FindGameObjectWithTag("Finish").transform;

            if (destination != null)
                agent.SetDestination(destination.position);
            else
                Debug.LogError(gameObject.name + ": cannot find the exit???");
        }
        // If the NPC knows to find the exit, check to see how close he is. Destroy him when he's there.
        else if (shoppingData.OutOfStock && destination != null)
        {
            if (Vector3.Distance(transform.position, destination.position) < 1f)
            {
                Destroy(gameObject);
            }
        }
        // If we haven't checked out yet and don't have a destination, set it to the nearest checkout location.
        else if (!shoppingData.CheckedOut && destination == null)
        {
            Debug.Log(gameObject.name + ": wants to checkout but has no checkout location. Finding one now...");
            destination = ShoppingHelper.GetNearestCheckoutLocation(transform).transform;

            if (destination != null)
                agent.SetDestination(destination.position);
            else
                Debug.LogError(gameObject.name + ": No valid checkout destination found...");
        }
        // If we haven't checked out and have a destination, keep track of our distance to it.
        // Once we're in range, interact with it.
        else if (!shoppingData.CheckedOut && destination != null)
        {
            // If the character is in range of the checkout, interact with it.
            if (Vector3.Distance(transform.position, destination.position) < 2f)
            {
                Checkout checkoutCounter = destination.GetComponentInParent<Checkout>();
                checkoutCounter.Interact(inventory, characterShoppingList);
                shoppingData.CheckedOut = true;
                destination = GameObject.FindGameObjectWithTag("Finish").transform;
                agent.SetDestination(destination.position);
            }
        }
        // If the NPC has checked out, they'll path to the store's exit.
        // Once there, they'll be destroyed.    
        else if (shoppingData.CheckedOut)
        {
            if (destination == null)
            {
                SFXManager.instance.source.PlayOneShot(SFXManager.instance.hello);
                destination = GameObject.FindGameObjectWithTag("Finish").transform;
                agent.SetDestination(destination.position);
            }

            if (Vector3.Distance(transform.position, destination.position) < 1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
