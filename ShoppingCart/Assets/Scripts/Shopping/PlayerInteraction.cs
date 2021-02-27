/*****************************************************************************
// File Name :         PlayerInteraction.cs
// Author :            Kyle Grenier
// Creation Date :     02/25/2021
//
// Brief Description : Script that handles how the player interacts with IInteractables.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CharacterInteraction))]
public class PlayerInteraction : MonoBehaviour
{
    CharacterInteraction characterInteraction;
    List<IItemInteractable> interactablesNearby = new List<IItemInteractable>();

    public event Action<IItemInteractable> Player_OnInteractableNearby;
    public event Action<IItemInteractable> Player_OnInteractableNearby_Removed;

    private PickupWindow pickupWindow = null;

    private void Awake()
    {
        characterInteraction = GetComponent<CharacterInteraction>();
        pickupWindow = FindObjectOfType<PickupWindow>();
    }

    private void Start()
    {
        if (pickupWindow != null)
            pickupWindow.Init(this, transform.GetComponentInParent<CharacterInventory>());
    }

    private void OnTriggerEnter(Collider col)
    {
        IItemInteractable i = col.GetComponent<IItemInteractable>();
        if (i != null)
        {
            if (!interactablesNearby.Contains(i))
            {
                interactablesNearby.Add(i);
                Player_OnInteractableNearby?.Invoke(i);
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (!col.CompareTag("Checkout"))
            return;

        Checkout checkout = col.GetComponent<Checkout>();
        if (checkout != null && Input.GetKeyDown(KeyCode.F))
        {
            checkout.Interact(GetComponentInParent<CharacterInventory>(), transform.parent.GetComponentInChildren<CharacterShoppingList>());
        }
    }

    private void OnTriggerExit(Collider col)
    {
        IItemInteractable i = col.GetComponent<IItemInteractable>();
        if (i != null)
        {
            if (interactablesNearby.Contains(i))
            {
                interactablesNearby.Remove(i);
                Player_OnInteractableNearby_Removed?.Invoke(i);
            }
        }
    }

    private void Update()
    {
        if (pickupWindow == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                for (int i = 0; i < interactablesNearby.Count; ++i)
                {
                    characterInteraction.Interact(interactablesNearby[i]);
                    //interactablesNearby.Remove(interactablesNearby[i]);
                }
            }
        }
    }
}
