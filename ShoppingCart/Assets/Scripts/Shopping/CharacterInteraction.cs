/*****************************************************************************
// File Name :         CharacterInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     02/23/2021
//
// Brief Description : Defines functionality for characters interacting with IInteractables.
*****************************************************************************/
using UnityEngine;
using System;

public class CharacterInteraction : MonoBehaviour
{
    [Tooltip("The ShoppingList to act as the sender when interacting with an IInteractable.")]
    [SerializeField] private ShoppingList listToSend;

    public event Action<IItemInteractable> OnInteracted;

    public void Interact(IItemInteractable interactable)
    {
        if (listToSend == null)
            Debug.LogWarning(gameObject.name + " has no game object to send to the interactable.");
        else
        {
            interactable.Interact(listToSend);
            OnInteracted?.Invoke(interactable);
        }
    }

    public void Interact(IInteractable interactable)
    {
        interactable.Interact();
    }
}
