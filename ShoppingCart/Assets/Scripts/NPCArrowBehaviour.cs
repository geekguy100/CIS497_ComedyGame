/*****************************************************************************
// File Name :         NPCArrowBehaviour.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class NPCArrowBehaviour : MonoBehaviour
{
    private CharacterInventory npcInventory;

    [Tooltip("The arrow to display above the NPC.")]
    [SerializeField] private GameObject arrow;


    private void Awake()
    {
        npcInventory = GetComponent<CharacterInventory>();
    }

    private void OnEnable()
    {
        npcInventory.OnItemAdded += CheckQuantities;
        npcInventory.OnCartEmptied += DeactivateArrow;
    }

    private void OnDisable()
    {
        npcInventory.OnItemAdded -= CheckQuantities;
        npcInventory.OnCartEmptied -= DeactivateArrow;
    }

    /// <summary>
    /// Checks to see if this NPC has enough of an item.
    /// If it does and the shopping center is out of it, display the arrow.
    /// </summary>
    /// <param name="itemType">The type of item to check for.</param>
    private void CheckQuantities(System.Type itemType)
    {
        if (!ShoppingHelper.IsOfTypeItem(itemType))
            return;

        int playerNeeds = ShoppingHelper.PlayerNeeds(itemType);

        // If the shopping center is out of the item we need, and this NPC has it, display the arrow.
        if (ShoppingCenter.instance.GetQuantity(itemType) < playerNeeds && npcInventory.GetQuantity(itemType) > 0)
            arrow.SetActive(true);
    }

    /// <summary>
    /// Deactives the arrow game object.
    /// </summary>
    private void DeactivateArrow()
    {
        arrow.SetActive(false);
    }
}
