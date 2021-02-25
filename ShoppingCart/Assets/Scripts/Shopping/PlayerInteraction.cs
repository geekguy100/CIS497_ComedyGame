/*****************************************************************************
// File Name :         PlayerInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Script that handles how the player interacts with IInteractables.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterInteraction))]
public class PlayerInteraction : MonoBehaviour
{
    CharacterInteraction characterInteraction;

    private void Awake()
    {
        characterInteraction = GetComponent<CharacterInteraction>();
    }

    // If the player is within the IInteractable
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Interactable"))
        {
            IInteractable interactable = col.GetComponent<IInteractable>();
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                print("CLICK");
                characterInteraction.Interact(interactable);
            }
        }
    }
}
