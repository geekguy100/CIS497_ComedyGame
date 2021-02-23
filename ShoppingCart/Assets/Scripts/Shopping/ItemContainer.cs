/*****************************************************************************
// File Name :         Item.cs
// Author :            Kyle Grenier
// Creation Date :     02/22/2021
//
// Brief Description : Class for defining item functionality.
*****************************************************************************/
using UnityEngine;

public class ItemContainer : MonoBehaviour, IInteractable
{
    [Tooltip("The item type that this container holds")]
    [SerializeField] private string itemType;

    void Start()
    {
        // Make sure this item container has a valid type associated with it.
        if (!ShoppingHelper.IsOfTypeItem(System.Type.GetType(itemType)))
        {
            Debug.LogWarning(gameObject.name + " cannot hold the non-Item type of " + itemType);
            Destroy(this);
            return;
        }

        Interact(gameObject);
    }

    public void Interact(GameObject sender)
    {
        Debug.Log("Item container holding " + itemType + "s has been interacted with by " + sender.name + "!");
        sender.GetComponent<ShoppingList>().AddItem(System.Type.GetType(itemType));
    }
}
