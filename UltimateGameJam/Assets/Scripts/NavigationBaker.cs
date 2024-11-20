using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class NavigationBaker : MonoBehaviour {

    public NavMeshSurface[] surfaces;
    public List<Transform> objectsToRotate;

    private float curZRot = 0;
    // Use this for initialization
    void Update () 
    {
        for (int i = 0; i < surfaces.Length; i++) 
        {
            surfaces[i].BuildNavMesh();    
        }    
    }

}