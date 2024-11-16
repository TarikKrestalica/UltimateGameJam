using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] uint goldAmount;
    [SerializeField] uint deathGold;
    [SerializeField] uint goldStealAmount;
    [Range(0, 10f)]
    [SerializeField] float movement;


    public virtual void OnDeath()
    {

    }

    public virtual void OnStealGold()
    {

    }
}
