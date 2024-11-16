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

    [SerializeField] GameObject targetPoint;

    void Update()
    {
        MoveToGoldStash();
    }

    public virtual void OnDeath()
    {

    }

    public virtual void OnStealGold()
    {

    }

    public virtual void MoveToGoldStash()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPoint.transform.position, movement * Time.deltaTime);
    }
}
