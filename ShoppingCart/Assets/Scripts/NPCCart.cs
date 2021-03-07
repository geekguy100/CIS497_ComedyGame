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
    [Tooltip("The magnitude of incoming force needed to stun the NPC.")]
    [SerializeField] private float stunMagnitude = 6f;

    private void OnCollisionEnter(Collision col)
    {
        // If the NPCs cart gets hit hard by the player's cart, make them stunned.
        if (col.gameObject.CompareTag("Cart") && col.gameObject.GetComponent<Rigidbody>().velocity.magnitude > stunMagnitude)
        {
            NPC npc = GetComponentInParent<NPC>();
            if (npc.myState == NPC.State.Stunned)
                return;

            print("PLAYER: STUNNED A BITCH");
            npc.myState = NPC.State.Stunned;
        }
    }
}
