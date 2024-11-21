using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

// Link to helper video: https://www.youtube.com/watch?v=mV-Uh_FEBn4
// Link to script: https://gist.githubusercontent.com/iwanPlays/7fdd3cde93adc84d67e28d4d11aa5370/raw/fae00b006b580859da768ebb6900b1fa96a328af/NavigationBaker.cs
public class NavigationBaker : MonoBehaviour {

    public NavMeshSurface[] surfaces;

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
