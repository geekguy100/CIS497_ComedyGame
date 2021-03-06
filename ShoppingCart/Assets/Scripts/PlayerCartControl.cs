/*****************************************************************************
// File Name :         PlayerCartControl.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CartControl))]
public class PlayerCartControl : MonoBehaviour
{
    private CartControl cartControl;
    public bool didAttach = false;
    private bool canPickupCart = true;
    private bool canAbandonCart = false;
    private Coroutine lastCoroutine = null;
    private GameObject cart = null;

    private void Awake()
    {
        cartControl = GetComponent<CartControl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cart != null && canPickupCart)
        {
            canPickupCart = false;

            if (lastCoroutine != null)
                StopCoroutine(lastCoroutine);

            cartControl.AssignCart(cart);

            lastCoroutine = StartCoroutine(ResetAbandonment());
            didAttach = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && cartControl.HasCart() && canAbandonCart)
        {
            canAbandonCart = false;
            if (lastCoroutine != null)
                StopCoroutine(lastCoroutine);

            cartControl.AbandonCart();

            // To make sure we won't immediately pick up another cart, we
            // start this coroutine.
            lastCoroutine = StartCoroutine(ResetPickup());
            didAttach = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Cart"))
            cart = col.gameObject;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Cart"))
            cart = null;
    }

    private void OnJointBreak(float breakForce)
    {
        canPickupCart = true;
        canAbandonCart = false;
        cart = null;
        didAttach = false;
    }

    private IEnumerator ResetPickup()
    {
        yield return new WaitForSeconds(0.2f);
        canPickupCart = true;
    }

    private IEnumerator ResetAbandonment()
    {
        yield return new WaitForSeconds(0.2f);
        canAbandonCart = true;
    }

}
