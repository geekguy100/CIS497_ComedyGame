using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCretrieving : NPCBehavior
{
    public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, ItemContainerData[] shoppingListData, CharacterInventory inventory, NPC.State myState)
    {
        if (npc.isTutorialNPC)
            return;

        //walk to your cart
        Debug.Log("I'm going to get my cart");
        cart.transform.position = whereIsMyCart;
        agent.destination = cart.transform.position;
    }
}
