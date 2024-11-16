using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] uint health;
    [SerializeField] uint goldAmount;
    [SerializeField] uint deathGold;
    [SerializeField] uint goldStealAmount;
    [Range(0, 10f)]
    [SerializeField] float movement;

    [SerializeField] GameObject targetPoint;

    private Vector3 startingPoint;

    void Start()
    {
        startingPoint = this.transform.position;
    }

    void Update()
    {
        if(targetPoint)
        {
            MoveToGoldStash();
            return;
        }

        RunBack();
    }

    public void TakeDamage(uint damage_amt)
    {
        if(health - damage_amt <= 0)
        {
            OnDeath();
        }

        health -= damage_amt;
    }

    public virtual void OnDeath()
    {
        uint totalCoinsEnemyLost = deathGold + goldStealAmount;
        GameManager.player.AddGold(totalCoinsEnemyLost);
        Destroy(this.gameObject);
    }

    public virtual void OnStealGold(GameObject g)
    {
        GameManager.player.TakeDamageToGoldStash(goldStealAmount);
        Destroy(g.gameObject); // For now.
    }

    public virtual void MoveToGoldStash()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPoint.transform.position, movement * Time.deltaTime);
    }

    public virtual void RunBack()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, startingPoint, movement * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Gold")
        {
            OnStealGold(collision.gameObject);
        }
    }
}
