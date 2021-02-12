/*****************************************************************************
// File Name :         CharacterMovement.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float gravity = 20f;

    private Vector3 movementDirection = Vector3.zero;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(h, 0, v) * movementSpeed;

        //CharacterController's Move method uses world-space, not local space, so we need to convert the inputocity to world space.
        input = transform.TransformDirection(input);

        if (controller.isGrounded)
        {
            movementDirection = input;
        }

        movementDirection.y -= gravity * Time.fixedDeltaTime;

        controller.Move(movementDirection * Time.fixedDeltaTime);
    }

}
