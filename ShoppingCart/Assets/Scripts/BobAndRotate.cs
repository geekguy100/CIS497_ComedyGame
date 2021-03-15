/*****************************************************************************
// File Name :         BobAndRotate.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class BobAndRotate : MonoBehaviour
{
    private float bobFrequency = 3f;
    private float bobAmplitude = 0.025f;
    private float rotationSpeed = 13f;

    private Vector3 localPos;
    private float startingYPos;

    private float counter;
    private float delay;
    private float currentDelayTime;

    private void Awake()
    {
        localPos = transform.localPosition;
        startingYPos = localPos.y;
    }

    private void Start()
    {
        // Initialize a delay before starting to bob.
        delay = Random.Range(0, 2f);
    }

    private void Update()
    {
        // Don't start bobbing until the delay is over.
        if (currentDelayTime < delay)
            currentDelayTime += Time.deltaTime;
        else
        {
            Bob();
            Rotate();
        }
    }
    
    void Bob()
    {
        counter += Time.deltaTime;
        localPos.y = Mathf.Sin(counter * bobFrequency) * bobAmplitude + startingYPos;
        transform.localPosition = localPos;

        if (counter > 360f)
            counter = 0;
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
