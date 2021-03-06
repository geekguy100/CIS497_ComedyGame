/*****************************************************************************
// File Name :         NPCShoppingData.cs
// Author :            Kyle Grenier
// Creation Date :     03/04/2021
//
// Brief Description : Holds NPCs shopping data (done shopping and index of item to search for).
*****************************************************************************/
using UnityEngine;

public class NPCShoppingData : MonoBehaviour
{
    private bool doneShopping = false;
    public bool DoneShopping { get { return doneShopping; } set { doneShopping = value; } }

    private bool checkedOut = false;
    public bool CheckedOut { get { return checkedOut; } set { checkedOut = value; } }

    private bool outOfStock = false;
    public bool OutOfStock { get { return outOfStock; } set { outOfStock = value; } }

    private int index = 0;
    public int Index
    {
        get { return index; }
        set
        {
            if (value < 0)
                index = 0;
            else
                index = value;
        }
    }
}
