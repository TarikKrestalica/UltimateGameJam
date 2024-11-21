using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.AI;

public class WallSetup : MonoBehaviour
{
    private void Start()
    {
        var modifier = gameObject.AddComponent<NavMeshModifier>();
        modifier.overrideArea = true;
        modifier.area = NavMesh.GetAreaFromName("Not Walkable"); // Ensure this matches your setup
    }
}

