using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class NavigationBaker : MonoBehaviour {

    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;

    private float curZRot = 0;
    // Use this for initialization
    void Update () 
    {
        for (int j = 0; j < objectsToRotate.Length; j++) 
        {
            curZRot += 15*Time.deltaTime;
            objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3 (0, 0, curZRot));
        }

        for (int i = 0; i < surfaces.Length; i++) 
        {
            surfaces[i].BuildNavMesh();    
        }    
    }

}
