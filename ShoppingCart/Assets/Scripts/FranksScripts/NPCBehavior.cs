using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBehavior : MonoBehaviour
{
    public abstract void NPCaction(NPC npc, NavMeshAgent agent, GameObject cart, Quaternion cartLocalRot, Vector3 cartLocalPos, Vector3 whereIsMyCart, bool hasDestination, int listIndex, ItemContainerData[] shoppingListArray, CharacterInventory inventory, NPC.State myState);
}
