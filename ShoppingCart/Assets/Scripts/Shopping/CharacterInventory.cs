/*****************************************************************************
// File Name :         CharacterInventory.cs
// Author :            Kyle Grenier
// Creation Date :     02/24/2021
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            AddItem(typeof(Watermelon));
        else if (Input.GetKeyDown(KeyCode.L))
            RemoveItem(typeof(Watermelon));
    }
}
