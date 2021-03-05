using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCresetting : NPCBehavior
{
    public override void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, ItemContainerData[] shoppingListArray, CharacterInventory inventory, NPC.State myState)
    {
        //reset cart's local rotation and position, create new joint, continue shopping
        Debug.Log("I'm picking up my cart");
        cart.transform.localPosition = cartLocalPos;
        cart.transform.localRotation = cartLocalRot;

        ReplaceJoint(cart);

        npc.hasDestination = false;
        npc.myState = NPC.State.Shopping;
    }

    private void ReplaceJoint(GameObject cart)
    {
        Joint newJoint = gameObject.AddComponent<SpringJoint>();

        newJoint.breakForce = 20;
        newJoint.breakTorque = 20;
        newJoint.connectedBody = cart.GetComponent<Rigidbody>();
        newJoint.anchor = new Vector3(0, 1, 0.7f);
        newJoint.connectedAnchor = new Vector3(0.01f, 0.9779999f, -0.538f); //don't look

        
    }
}
