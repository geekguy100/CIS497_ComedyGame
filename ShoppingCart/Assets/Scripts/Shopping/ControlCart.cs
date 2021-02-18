/*****************************************************************************
// File Name :         ControlCart.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class ControlCart : MonoBehaviour
{
    // The joint to hold the cart to the character. Null if we are not in control of a cart.
    private FixedJoint joint;

    // The cart we are controlling. Null if we are not in control of a cart.
    private GameObject cart;

    [Tooltip("How many units in front of the character should the cart be?")]
    [SerializeField] private float cartOffset = 1.5f;

    [Tooltip("If the joint encounters this much force, break it.")]
    [SerializeField] private float grabBreakingForce = 100f;

    [Tooltip("If the joint encounters this much torque, break it.")]
    [SerializeField] private float grabBreakingTorque = 100f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && cart != null)
            AbandonCart();
    }

    private void OnTriggerStay(Collider col)
    {
        if (Input.GetKey(KeyCode.E) && col.gameObject.CompareTag("Cart") && cart == null)
        {
            AssignCart(col.gameObject);
        }
    }

    private void AssignCart(GameObject cart)
    {
        this.cart = cart;
        print("Assign");

        cart.transform.rotation = transform.rotation;

        //Set the cart's position to the character's position but forward cartOffset units.
        Vector3 pos = new Vector3(transform.position.x, cart.transform.position.y, transform.position.z) + (transform.forward * cartOffset);
        cart.transform.position = pos;

        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = cart.GetComponent<Rigidbody>();
        joint.breakForce = grabBreakingForce;
        joint.breakTorque = grabBreakingTorque;

        Collider cartCollider = cart.GetComponent<Collider>();

        // Make sure any colliders on the character do not collide with the cart.
        foreach (var collider in GetComponents<Collider>())
        {
            Physics.IgnoreCollision(collider, cartCollider, true);
        }

    }

    private void AbandonCart()
    {
        print("BREAK");
        // If there is a joint still connected to the cart, destroy it.
        if (joint != null)
            Destroy(joint);

        // If the cart is no longer there, return.
        if (cart == null)
        {
            print("There is no cart...");
            return;
        }

        Collider cartCollider = cart.GetComponent<Collider>();

        // Reenable collisions between the character and the cart.
        foreach (var collider in GetComponents<Collider>())
        {
            Physics.IgnoreCollision(collider, cartCollider, false);
        }

        cart = null;
    }

    private void OnJointBreak(float breakForce)
    {
        // Abandon the cart when the joint breaks.
        AbandonCart();
    }
}
