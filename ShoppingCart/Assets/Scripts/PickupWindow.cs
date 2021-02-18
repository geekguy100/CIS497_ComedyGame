using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupWindow : MonoBehaviour
{
    public GameObject player;
    public GameObject cart;
    public List<ItemCollection> nearbyItems;
    public Text display;
    public GameObject outline;
    public int selection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cart = GameObject.FindGameObjectWithTag("Cart");
        display.text = "";
        selection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y < 0 && selection < nearbyItems.Count - 1)
        {
            selection++;
            Debug.Log(selection);
        }
        if (Input.mouseScrollDelta.y > 0 && selection != 0)
        {
            selection--;
            Debug.Log(selection);
        }
        if (Input.GetKeyDown(KeyCode.F) && nearbyItems.Count != 0)
        {
            nearbyItems[selection].transform.position = cart.transform.position + new Vector3(0, 1, 0);
            nearbyItems[selection].isInPlayerCart = true;
            selection = 0;
        }
    }

    public void UpdateDisplay()
    {
        display.text = "";
        foreach (ItemCollection i in nearbyItems)
        {
            display.text += "\n" + i.name;
        }
    }
}
