using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPC : MonoBehaviour
{
    CharacterInventory list;
    // Start is called before the first frame update
    void Start()
    {
        list = gameObject.GetComponent<CharacterInventory>();
        for(int i = 0; i < 4; i++)
        {
            list.AddItem(System.Type.GetType(ShoppingCenter.instance.GetRandomItem().ItemType));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
