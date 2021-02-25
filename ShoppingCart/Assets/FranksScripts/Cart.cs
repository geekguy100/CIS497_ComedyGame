using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cart : MonoBehaviour
{
    [SerializeField] GameObject owner;
    private Vector3 ownerPos;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localRotation.z >= 25 || transform.localRotation.z <= -25)
        {
            Debug.Log("My cart tipped over!");
        }
    }
}
