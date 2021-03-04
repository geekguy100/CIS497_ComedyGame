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
    [SerializeField] GameObject cart;
    private Item[] potentialItems = { new Orange(), new CoughSyrup(), new Laptop(), new Milk(), new Steak(), new Shampoo() };


    public enum State { Shopping, RetreivingCart, PickingUpCart}
    public State myState { get; set; }
    public int listIndex { get; set; }
    public bool hasDestination { get; set; }

    private Vector3 whereIsMyCart;
    private Vector3 cartLocalPos;
    private Quaternion cartLocalRot;

    private NPCBehavior currenBehavior;

    NavMeshAgent agent;
    NavMeshObstacle obstacle;


    void Start()
    {
        hasDestination = false;
        listIndex = 0;

        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        

        cartLocalPos = cart.transform.localPosition;
        cartLocalRot = cart.transform.localRotation;

        myState = State.Shopping;

        currenBehavior = gameObject.AddComponent<NPCshopping>();


        //move to GM later, makes NPC and carts not collide1
        Physics.IgnoreLayerCollision(6, 7, true);


    }

    // Update is called once per frame
    void Update()
    {
        PerformNPCAction();

        switch (myState)
        {
            case State.Shopping:

                if(gameObject.GetComponent<NPCshopping>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currenBehavior = gameObject.AddComponent<NPCshopping>();
                }

                break;

            case State.RetreivingCart:

                if (gameObject.GetComponent<NPCretrieving>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currenBehavior = gameObject.AddComponent<NPCretrieving>();
                }

                break;

            case State.PickingUpCart:

                if (gameObject.GetComponent<NPCresetting>() == null)
                {
                    Destroy(GetComponent<NPCBehavior>());
                    currenBehavior = gameObject.AddComponent<NPCresetting>();
                }

                //hasDestination = false;
                //myState = State.Shopping;


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
        currenBehavior.NPCaction(GetComponent<NPC>(), agent, cart, cartLocalRot, cartLocalPos, whereIsMyCart, hasDestination, listIndex, potentialItems, myState);
    }
    
}
