using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cart : MonoBehaviour
{
    [SerializeField] GameObject owner;
    private Vector3 ownerPos;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.updateRotation = false;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit cast;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out cast, 500))
            {
                agent.destination = cast.point;
            }
        }
    }
}
