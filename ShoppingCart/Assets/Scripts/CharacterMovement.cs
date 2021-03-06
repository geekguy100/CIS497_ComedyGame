/*****************************************************************************
// File Name :         CharacterMovement.cs
// Author :            Kyle Grenier
// Creation Date :     02/17/2021
//
// Brief Description : Rigidbody character movement controller.
*****************************************************************************/
using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashCooldown = 4f;

    // True if the dash sequence is complete; True if the character is able to perform a dash.
    private bool dashComplete = true;

    private bool performingDash = false;
    // True if the character is mid-dash.
    public bool IsDashing { get { return performingDash; } }

    private Vector3 movementDirection = Vector3.zero;

    private Rigidbody rb;

    public event Action<float> OnDashUpdate;

    private float dashTime;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dashTime = dashCooldown;
    }

    public void Move(Vector3 direction)
    {
        // Prevent movement if the character is mid-dash.
        if (performingDash)
            return;

        //rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
        rb.velocity = new Vector3(direction.x * movementSpeed, rb.velocity.y, direction.z * movementSpeed);
    }

    /// <summary>
    /// Boosts the character forward.
    /// </summary>
    public void Dash()
    {
        if (!dashComplete)
            return;

        StartCoroutine(PerformDash());
    }

    private void Update()
    {
        if (!performingDash && dashTime < dashCooldown)
        {
            dashTime += Time.deltaTime;
            OnDashUpdate?.Invoke(dashTime / dashCooldown);
        }
    }

    /// <summary>
    /// Perform the dash.
    /// </summary>
    private IEnumerator PerformDash()
    {
        dashComplete = false;

        performingDash = true;
        rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
        dashTime = 0;
        OnDashUpdate?.Invoke(dashTime);

        yield return new WaitForSeconds(dashDuration);
        performingDash = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);
        dashComplete = true;
    }

}
