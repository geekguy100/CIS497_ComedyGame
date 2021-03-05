/*****************************************************************************
// File Name :         NPCShoppingData.cs
// Author :            Kyle Grenier
// Creation Date :     03/04/2021
//
// Brief Description : Holds the index of the NPC's shopping list.
*****************************************************************************/
using UnityEngine;

public class NPCShoppingData : MonoBehaviour
{
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
