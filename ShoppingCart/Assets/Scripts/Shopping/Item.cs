/*****************************************************************************
// File Name :         Item.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    public virtual void Interact(GameObject sender)
    {
        CharacterInventory inventory = sender.GetComponent<CharacterInventory>();
        if (inventory != null)
        {
            inventory.AddItem(GetType());
            Destroy(gameObject);
        }
    }

    protected abstract string GetDescription();
}
