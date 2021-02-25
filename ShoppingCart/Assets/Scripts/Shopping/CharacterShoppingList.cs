/*****************************************************************************
// File Name :         CharacterShoppingList.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : The shopping list each character has on their person.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CharacterShoppingList : ShoppingList
{
    [Tooltip("Optional UI associated with this particular shopping list.")]
    [SerializeField] private ShoppingListUI shoppingListUI;

    private void Start()
    {
        
    }
}
