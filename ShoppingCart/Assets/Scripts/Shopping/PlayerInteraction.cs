/*****************************************************************************
// File Name :         PlayerInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
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
        IInteractable interactable = col.GetComponent<IInteractable>();
        if (interactable != null && Input.GetMouseButtonDown(0))
            characterInteraction.Interact(interactable);
    }
}
