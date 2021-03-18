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

    public bool grabbedItem;

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
        else if (col.CompareTag("CheckoutLocation") && checkout == null)
        {
            checkout = col.GetComponent<Checkout>();
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
        else if (col.CompareTag("CheckoutLocation") && checkout != null)
            checkout = null;
    }

    private Checkout checkout;
    private void Update()
    {
        if (pickupWindow == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                grabbedItem = true;
                for (int i = 0; i < interactablesNearby.Count; ++i)
                {
                    characterInteraction.Interact(interactablesNearby[i]);
                    //interactablesNearby.Remove(interactablesNearby[i]);
                }
            }
        }

        if (checkout != null && Input.GetKeyDown(KeyCode.F))
        {
            checkout.Interact(GetComponentInParent<CharacterInventory>(), transform.parent.GetComponentInChildren<CharacterShoppingList>());
        }


    }
}
