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
    private Coroutine lastCoroutine = null;

    private void Awake()
    {
        cartControl = GetComponent<CartControl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cartControl.HasCart())
        {
            if (lastCoroutine != null)
                StopCoroutine(lastCoroutine);

            cartControl.AbandonCart();

            // To make sure we won't immediately pick up another cart, we
            // start this coroutine.
            lastCoroutine = StartCoroutine(ResetPickup());
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (Input.GetKey(KeyCode.E) && col.gameObject.CompareTag("Cart") && !cartControl.HasCart() && canPickupCart)
        {
            cartControl.AssignCart(col.gameObject);
            canPickupCart = false;
            didAttach = true;
        }
    }

    private void OnJointBreak(float breakForce)
    {
        canPickupCart = true;
    }

    private IEnumerator ResetPickup()
    {
        yield return new WaitForSeconds(0.2f);
        canPickupCart = true;
    }

}
