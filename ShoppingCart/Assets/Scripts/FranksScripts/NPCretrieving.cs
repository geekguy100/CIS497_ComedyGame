using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCretrieving : NPCBehavior
{
    public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, Item[] potentialItems, NPC.State myState)
    {
        //walk to your cart
        Debug.Log("I'm going to get my cart");
        cart.transform.position = whereIsMyCart;
        agent.destination = cart.transform.position;
    }
}
