using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public bool isOnShelf;
    public bool isInCart;
    public float distAway;
    public float reach;
    public GameObject player;
    public GameObject cart;

    // Start is called before the first frame update
    void Start()
    {
        isOnShelf = true;
        isInCart = false;
        reach = 2;
        player = GameObject.FindGameObjectWithTag("Player");
        cart = GameObject.FindGameObjectWithTag("Cart");
    }

    // Update is called once per frame
    void Update()
    {
        distAway = Vector3.Distance(player.transform.position, transform.position);
        if (isOnShelf && distAway < reach && Input.GetKeyDown(KeyCode.E))
        {
            isOnShelf = false;
            transform.position = cart.transform.position + new Vector3(0, 1, 0);
            isInCart = true;
        }
    }
}
