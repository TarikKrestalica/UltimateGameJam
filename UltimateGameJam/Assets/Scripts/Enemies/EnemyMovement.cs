using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        if(!agent)
        {
            Debug.LogError("Navmesh Not found!");
            return;
        }
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.isOnNavMesh)
        {
            Debug.LogError("NavMesh Not found!");
            return;
        }

        agent.SetDestination(targetPoint.transform.position);
    }
}
