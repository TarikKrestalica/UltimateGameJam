using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract string Name { get; }  // abstract field, getter.
    public abstract float FireRate { get; }  
    public abstract float DamageBoost { get; } 
    public abstract float DamageAmount { get; } 
    public abstract float DamageBoostMultiplier { get; } 


    public virtual void Fire()
    {

    }

    public virtual void UpgradeDamage()
    {

    }

}
