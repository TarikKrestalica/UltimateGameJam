using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] uint goldAmount;

    // [SerializeField] Weapon equippedWeapon;

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnDeath()
    {
        
    }
}
