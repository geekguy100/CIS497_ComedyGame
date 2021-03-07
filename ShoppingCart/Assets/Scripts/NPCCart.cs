/*****************************************************************************
// File Name :         NPCCart.cs
// Author :            Kyle Grenier
// Creation Date :     03/06/2021
//
// Brief Description : Behavior for the player ramming into the NPCs cart.
*****************************************************************************/
using UnityEngine;

public class NPCCart : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        // If the NPCs cart gets hit by the player's cart while the player is dashing, make them stunned.
        if (col.gameObject.CompareTag("Cart") && GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().IsDashing)
        {
            NPC npc = GetComponentInParent<NPC>();
            if (npc.myState == NPC.State.Stunned)
                return;

            npc.myState = NPC.State.Stunned;
        }
    }
}
