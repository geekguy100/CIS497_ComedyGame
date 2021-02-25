/*****************************************************************************
// File Name :         CharacterShoppingList.cs
// Author :            Kyle Grenier
// Creation Date :     02/24/2021
//
// Brief Description : The list of goods the character needs to obtain.
*****************************************************************************/
using UnityEngine;

public class CharacterShoppingList : ShoppingList
{
    const int ITEMS_TO_ADD = 5;
    private void Start()
    {
        for (int i = 0; i < ITEMS_TO_ADD; ++i)
        {
            int r = Random.Range(0, 2);
            if (r == 0)
                AddItem(typeof(Orange));
            else
                AddItem(typeof(Watermelon));
        }
    }
}
