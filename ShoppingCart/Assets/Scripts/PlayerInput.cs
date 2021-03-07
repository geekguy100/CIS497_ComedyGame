/*****************************************************************************
// File Name :         PlayerInput.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    //The CharacterMovement script to handle moving the character.
    private CharacterMovement characterMovement;
    public PlayerCartControl playerCartControl;

    //The player's input.
    private Vector3 input = Vector3.zero;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        playerCartControl = GetComponent<PlayerCartControl>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        input = new Vector3(h, 0, v);
        input = transform.TransformDirection(input);

        if (Input.GetKeyDown(KeyCode.LeftShift) && playerCartControl.didAttach)
            characterMovement.Dash();
    }

    private void FixedUpdate()
    {
        //Applying movement in FixedUpdate b/c we're dealing with Rigidbodies.
        characterMovement.Move(input);
    }
}
