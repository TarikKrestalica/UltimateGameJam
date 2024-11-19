using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Sample Tutorial: https://youtu.be/HRX0pUSucW4?si=Fn7Gt00F1cjVd6sv
public class EnemyAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject target;
    void Start()	{
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

        agent.SetDestination(target.transform.position);
    }
}
