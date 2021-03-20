/*****************************************************************************
// File Name :         ArrowScaler.cs
// Author :            Kyle Grenier
// Creation Date :     03/19/2021
//
// Brief Description : Scales the arrow with distance to the player.
*****************************************************************************/
using UnityEngine;

public class ArrowScaler : MonoBehaviour
{
    private GameObject player;
    private float maxMagnitude;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("ARROW: Cannot find player so cannot calc. distance to them.");
            Destroy(this);
            return;
        }

        maxMagnitude = transform.localScale.magnitude;
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 scale = Vector3.one * distance / 10;

        // Clamping the max scale to whatever the arrow is initially set to.
        scale = Vector3.ClampMagnitude(scale, maxMagnitude);

        transform.localScale = scale;
    }
}
