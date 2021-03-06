/*****************************************************************************
// File Name :         NPCCart.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class NPCCart : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        // If the NPCs cart gets hit hard by the player's cart, make them stunned.
        if (col.gameObject.CompareTag("Cart") && col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 4f)
        {
            NPC npc = GetComponentInParent<NPC>();
            if (npc.myState == NPC.State.Stunned)
                return;

            print("PLAYER: STUNNED A BITCH");
            npc.myState = NPC.State.Stunned;
        }
    }
}
