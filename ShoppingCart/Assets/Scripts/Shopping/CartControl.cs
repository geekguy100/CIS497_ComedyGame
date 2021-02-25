/*****************************************************************************
// File Name :         ControlCart.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : Allows a game object to control a shopping cart.
*****************************************************************************/
using UnityEngine;

public class CartControl : MonoBehaviour
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


    /// <summary>
    /// Assigns a shopping cart to the character.
    /// </summary>
    /// <param name="cart"></param>
    public void AssignCart(GameObject cart)
    {
        this.cart = cart;

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

    public void AbandonCart()
    {
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

    /// <summary>
    /// Does the character have a cart they are in control of?
    /// </summary>
    /// <returns>True if the character has a cart they are in control of.</returns>
    public bool HasCart()
    {
        return (cart != null);
    }

    private void OnJointBreak(float breakForce)
    {
        // Abandon the cart when the joint breaks.
        AbandonCart();
    }
}
