using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPC : MonoBehaviour
{
    CharacterInventory list;

    private void OnEnable()
    {
        EventManager.OnGameStart += Init;
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= Init;
    }

    private void Init()
    {
        list = gameObject.GetComponent<CharacterInventory>();
        for (int i = 0; i < 4; i++)
        {
            list.AddItem(System.Type.GetType(ShoppingCenter.instance.GetRandomItem().ItemType));
        }
    }
}
