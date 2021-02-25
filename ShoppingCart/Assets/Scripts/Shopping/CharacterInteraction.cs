/*****************************************************************************
// File Name :         CharacterInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     02/23/2021
//
// Brief Description : Defines functionality for characters interacting with IInteractables.
*****************************************************************************/
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    [Tooltip("The GameObject to act as the sender when interacting with an IInteractable.")]
    [SerializeField] private GameObject gameObjectToSend;

    public void Interact(IInteractable interactable)
    {
        if (gameObjectToSend == null)
            Debug.LogWarning(gameObject.name + " has no game object to send to the interactable.");
        else
            interactable.Interact(gameObjectToSend);
    }
}
