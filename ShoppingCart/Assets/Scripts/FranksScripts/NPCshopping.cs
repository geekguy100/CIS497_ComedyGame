using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCshopping : NPCBehavior
{
    public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, Item[] potentialItems, NPC.State myState)
    {
        //reset cart if it tips or turns weird (currently not working)
        if (Mathf.Abs(cart.transform.localRotation.z) >= 25 || Mathf.Abs(cart.transform.localRotation.y) >= 45)
        {
            Debug.Log("My cart tipped over!");
            cart.transform.localRotation = cartLocalRot;
            Debug.Log("Adjusted my cart");
        }

        //if you don't have a destination, set one and begin walking there
        if (hasDestination == false)
        {
            agent.SetDestination(ShoppingHelper.GetNearestContainerOfType(GetComponent<Transform>(), potentialItems[listIndex].GetType()).position);
            npc.hasDestination = true;
        }

        //if you're at your destination, say you no longer have a destination, and choose your next one
        if (Mathf.Abs(transform.position.x - ShoppingHelper.GetNearestContainerOfType(GetComponent<Transform>(), potentialItems[listIndex].GetType()).position.x) <= 5
            || Mathf.Abs(transform.position.z - ShoppingHelper.GetNearestContainerOfType(GetComponent<Transform>(), potentialItems[listIndex].GetType()).position.z) <= 5)
        {
            if (listIndex < potentialItems.Length - 1) npc.listIndex++;
            else npc.listIndex = 0;
            npc.hasDestination = false;
        }
    }
}
