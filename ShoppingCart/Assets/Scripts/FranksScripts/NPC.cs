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


    enum State { Shopping, RetreivingCart, PickingUpCart}
    private State myState;
    private Vector3 whereIsMyCart;
    private SpringJoint myJoint;
    private SpringJoint newJoint;

    private Vector3 cartLocalPos;
    private Quaternion cartLocalRot;

    NavMeshAgent agent;
    void Start()
    {
        cartLocalPos = cart.transform.localPosition;
        cartLocalRot = cart.transform.localRotation;
        myJoint = gameObject.GetComponent<SpringJoint>();

        myState = State.Shopping;
        agent = GetComponent<NavMeshAgent>();


        //move to GM later, makes NPC and carts not collide1
        Physics.IgnoreLayerCollision(6, 7, true);
    }

    // Update is called once per frame
    void Update()
    {
        

        switch (myState)
        {
            case State.Shopping:

                if (cart.transform.rotation.z >= 25 || cart.transform.rotation.z <= -25)
                {
                    Debug.Log("My cart tipped over!");
                    cart.transform.localRotation = cartLocalRot;
                    Debug.Log("Adjusted my cart");
                }

                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit cast;

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out cast, 500))
                    {
                        agent.destination = cast.point;
                    }
                }

                break;

            case State.RetreivingCart:

                Debug.Log("I'm going to get my cart");
                cart.transform.position = whereIsMyCart;
                agent.destination = cart.transform.position;
                break;

            case State.PickingUpCart:
                Debug.Log("I'm picking up my cart");
                cart.transform.localPosition = cartLocalPos;
                cart.transform.localRotation = cartLocalRot;

                ReplaceJoint();

                myState = State.Shopping;
                
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
        if(myState == State.RetreivingCart)
        {
            if(collision.gameObject == cart)
            {
                myState = State.PickingUpCart;
            }
        }
    }

    private void ReplaceJoint()
    {
        newJoint = gameObject.AddComponent<SpringJoint>();

        newJoint.breakForce = 20;
        newJoint.breakTorque = 20;
        newJoint.connectedBody = cart.GetComponent<Rigidbody>();
        newJoint.anchor = new Vector3(0, 1, 0.7f);
        newJoint.connectedAnchor = new Vector3(0.01f, 0.9779999f, -0.538f);
    }
}
