using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableBomb : Consumable
{
    [SerializeField] static int staticCount;

    [Range(0, 20f)]
    [SerializeField] float explosionRadius;
    
    protected override void OnConsume()
    {

    }

    protected override void DecrementCount()
    {
        
    }
}
 