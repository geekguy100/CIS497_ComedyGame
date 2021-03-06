/* Frank Calabrese
 * NPC.cs
 * state machine for NPCs. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private GameObject cart;
    //private Item[] potentialItems = { new Orange(), new CoughSyrup(), new Laptop(), new Milk(), new Steak(), new Shampoo() };
    // The list of items the NPC needs.
    private CharacterShoppingList shoppingList;
    private ItemContainerData[] shoppingListData; // Shopping list in array format.

    // The list of items the NPC has on their person.
    private CharacterInventory inventory;


    public enum State { Shopping, RetreivingCart, PickingUpCart, Checkout, Stunned}
    public State myState { get; set; }
    public int listIndex { get; set; }
    public bool hasDestination { get; set; }

    private Vector3 whereIsMyCart;
    private Vector3 cartLocalPos;
    private Quaternion cartLocalRot;

    private NPCBehavior currentBehavior;

    NavMeshAgent agent;
    NavMeshObstacle obstacle;

    // True if the NPC is set up with a populated list and initialized values.
    private bool setup = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        shoppingList = GetComponent<CharacterShoppingList>();
        shoppingList.OnListPopulated += Setup;
        inventory = GetComponent<CharacterInventory>();

        currentBehavior = gameObject.AddComponent<NPCshopping>();

        cart = transform.GetChild(0).gameObject;
    }

    private void OnDisable()
    {
        shoppingList.OnListPopulated -= Setup;
    }

    void Start()
    {
        //move to GM later, makes NPC and carts not collide1
        Physics.IgnoreLayerCollision(6, 7, true);
    }

    /// <summary>
    /// Setup the NPC after its list is populated.
    /// </summary>
    void Setup()
    {
        shoppingListData = shoppingList.GetItemData();

        hasDestination = false;
        listIndex = 0;


        obstacle.enabled = false;


        cartLocalPos = cart.transform.localPosition;
        cartLocalRot = cart.transform.localRotation;

        myState = State.Shopping;

        foreach (ItemContainerData item in shoppingListData)
            print("ITEM: " + item);

        setup = true;
    }




    // Update is called once per frame
    void Update()
    {
        // Don't run until setup is complete!
        if (!setup)
            return;


        PerformNPCAction();

        switch (myState)
        {
            case State.Shopping:

                if(gameObject.GetComponent<NPCshopping>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currentBehavior = gameObject.AddComponent<NPCshopping>();
                }

                break;

            case State.RetreivingCart:

                if (gameObject.GetComponent<NPCretrieving>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currentBehavior = gameObject.AddComponent<NPCretrieving>();
                }

                break;

            case State.PickingUpCart:

                if (gameObject.GetComponent<NPCresetting>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currentBehavior = gameObject.AddComponent<NPCresetting>();
                }

                //hasDestination = false;
                //myState = State.Shopping;


                break;

            case State.Checkout:

                if (gameObject.GetComponent<NPCcheckout>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currentBehavior = gameObject.AddComponent<NPCcheckout>();
                }

                break;

            case State.Stunned:
                if (gameObject.GetComponent<NPCstunned>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currentBehavior = gameObject.AddComponent<NPCstunned>();
                }

                break;
            default:

                Debug.LogError("The NPCs state machine broke :(");
                break;
        }

    }

    private void OnJointBreak(float breakForce)
    {
        whereIsMyCart = new Vector3(cart.transform.position.x, cart.transform.position.y, cart.transform.position.z);
        myState = State.RetreivingCart;
        agent.destination = cart.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if you collide with your cart while retrieving it, go to pick up state
        if(myState == State.RetreivingCart)
        {
            if(collision.gameObject == cart)
            {
                myState = State.PickingUpCart;
            }
        }
    }

    

    private void PerformNPCAction()
    {
        currentBehavior.NPCaction(GetComponent<NPC>(), agent, cart, cartLocalRot, cartLocalPos, whereIsMyCart, hasDestination, listIndex, shoppingListData, inventory, myState);
    }
    
}
