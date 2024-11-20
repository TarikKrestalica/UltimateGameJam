using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableBond : Consumable
{
    [SerializeField] static int staticCount;

    [Range(0, int.MaxValue)]
    [SerializeField] float bondAmount;

    [Range(0, 1f)]
    [SerializeField] static float growthRate;

    protected override void OnConsume()
    {

    }

    protected override void DecrementCount()
    {
        
    }
}
