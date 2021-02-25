/*****************************************************************************
// File Name :         CharacterInventory.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : The inventory of a character; what that character has on their person.
*****************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CharacterInventory : ShoppingList
{
    private void Start()
    {
        AddItem(typeof(Orange));
    }
}
