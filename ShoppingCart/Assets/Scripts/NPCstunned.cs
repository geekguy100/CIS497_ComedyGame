/*****************************************************************************
// File Name :         NPCstunned.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCstunned : NPCBehavior
{
    private CharacterInventory inventory;
    private NPCShoppingData shoppingData;

    private NavMeshAgent agent;


    private bool stunned = false;

    private void Awake()
    {
        inventory = GetComponent<CharacterInventory>();
        shoppingData = GetComponent<NPCShoppingData>();

        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        /*int num = Random.Range(0, 2);
        if (num == 0)
            SFXManager.instance.source.PlayOneShot(SFXManager.instance.hit);
        else
            SFXManager.instance.source.PlayOneShot(SFXManager.instance.error);*/
        SFXManager.instance.source.PlayOneShot(SFXManager.instance.hit);
    }

    // NPC is stunned for a few seconds. He just sits there while stunned.
    // After stun duration, all items go back into his inventory.
    public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, ItemContainerData[] shoppingListArray, CharacterInventory inventory, NPC.State myState)
    {
        if (stunned)
            return;

        agent.isStopped = true;
        ItemContainerData[] inventoryItems = inventory.GetItemData();
        GameObject prefab = Resources.Load("LooseItem") as GameObject;

        // For each item in the NPCs inventory, create a loose item on the ground.
        for (int i = 0; i < inventoryItems.Length; ++i)
        {
            // Spawn in a loose item, and initialize it with the proper item type and quantity.
            ItemContainer looseItem = Instantiate(prefab, transform.position + Vector3.up * 5f, prefab.transform.rotation).GetComponent<ItemContainer>();
            looseItem.Init(inventoryItems[i].ItemType, inventoryItems[i].Quantity);

            // Add a random force to the spawned loose item for effect.
            //Vector3 force = new Vector3(Random.Range(-3f, 3f), 2f, Random.Range(-3f, 3f));
            //looseItem.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);

            // Remove the right amount of the lost item from our inventory.
            for (int j = 0; j < inventoryItems[i].Quantity; ++j)
            {
                inventory.RemoveItem(System.Type.GetType(inventoryItems[i].ItemType));
            }
        }
        SFXManager.instance.source.PlayOneShot(SFXManager.instance.drop);
        // All items have been removed from the NPCs inventory.
        // Start the stun cooldown.
        StartCoroutine(StunCooldown(npc));
    }

    private IEnumerator StunCooldown(NPC npc)
    {
        stunned = true;
        yield return new WaitForSeconds(5f);

        // After NPC is stunned, resume shopping -- he'll go pick up any loose items by nature.
        shoppingData.Index = 0;
        agent.isStopped = false;
        npc.myState = NPC.State.PickingUpCart;// changed from shopping
    }

    // In case this script is disabled early (by another behavior taking priority),
    // make sure to reset the shopping data index to 0 and allow the NPC to move.
    private void OnDisable()
    {
        agent.isStopped = false;
        shoppingData.Index = 0;
    }
}
