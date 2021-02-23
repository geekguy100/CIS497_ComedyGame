/*****************************************************************************
// File Name :         IInteractable.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Interface for objects that can be interacted with.
*****************************************************************************/
using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject sender);
}
