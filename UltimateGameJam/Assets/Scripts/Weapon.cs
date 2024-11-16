using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private abstract string Name { get; }  // abstract field, getter.
    private abstract float FireRate { get; }  
    private abstract float DamageBoost { get; } 
    private abstract float DamageAmount { get; } 
    virtual abstract float DamageBoostMultiplier { get; } 


    public virtual void Fire()
    {

    }

    public virtual void UpgradeDamage()
    {

    }

}
