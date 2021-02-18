using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject cart;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        //move to GM later, makes NPC and carts not collide
        Physics.IgnoreLayerCollision(6, 7, true);

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit cast;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out cast, 500))
            {
                agent.destination = cast.point;
            }
        }

       
    }

}
