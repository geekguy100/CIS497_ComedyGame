/*****************************************************************************
// File Name :         IInteractable.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Interface for objects that can be interacted with.
*****************************************************************************/
using UnityEngine;

public interface IItemInteractable
{
    void Interact(ShoppingList senderList);
}
