/*****************************************************************************
// File Name :         CharacterMovement.cs
// Author :            Kyle Grenier
// Creation Date :     02/17/2021
//
// Brief Description : Rigidbody character movement controller.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 6f;

    private Vector3 movementDirection = Vector3.zero;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        //rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
        rb.velocity = new Vector3(direction.x * movementSpeed, rb.velocity.y, direction.z * movementSpeed);
    }

}
