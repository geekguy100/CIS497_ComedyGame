/*****************************************************************************
// File Name :         Item.cs
// Author :            Kyle Grenier
// Creation Date :     02/26/2021
//
// Brief Description : Ground item behavior.
*****************************************************************************/
using UnityEngine;

public abstract class Item : MonoBehaviour, IItemInteractable
{
    public virtual void Interact(ShoppingList senderList)
    {
        if (senderList != null)
        {
            senderList.AddItem(GetType());
            Destroy(gameObject);
        }
    }

    protected abstract string GetDescription();
}
