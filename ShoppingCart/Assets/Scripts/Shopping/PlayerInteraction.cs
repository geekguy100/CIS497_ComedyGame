/*****************************************************************************
// File Name :         PlayerInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Script that handles how the player interacts with IInteractables.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterInteraction))]
public class PlayerInteraction : MonoBehaviour
{
    CharacterInteraction characterInteraction;
    List<IInteractable> interactablesNearby = new List<IInteractable>();

    private void Awake()
    {
        characterInteraction = GetComponent<CharacterInteraction>();
    }

    //// If the player is within the IInteractable
    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.CompareTag("Interactable"))
    //    {
    //        IInteractable interactable = col.GetComponent<IInteractable>();
    //        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
    //        {
    //            characterInteraction.Interact(interactable);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider col)
    {
        IInteractable i = col.GetComponent<IInteractable>();
        if (i != null)
        {
            if (!interactablesNearby.Contains(i))
                interactablesNearby.Add(i);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        IInteractable i = col.GetComponent<IInteractable>();
        if (i != null)
        {
            if (interactablesNearby.Contains(i))
                interactablesNearby.Remove(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < interactablesNearby.Count; ++i)
            {
                characterInteraction.Interact(interactablesNearby[i]);
                interactablesNearby.Remove(interactablesNearby[i]);
            }
        }
    }
}
