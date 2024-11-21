using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    public void Consume()
    {
        OnConsume();
        DecrementCount();
    }
    protected abstract void OnConsume();
    protected abstract void DecrementCount();
}
