using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static float GoldBoost = 1;
    private float startingHealth;
    [SerializeField] float health;
    [SerializeField] uint goldAmount;
    [SerializeField] uint deathGold;
    [SerializeField] uint goldStealAmount;
    
    [Range(0, 10f)]
    [SerializeField] float movement;
    [SerializeField] GameObject targetPoint;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] GameObject healthBar;

    private Vector3 startingPoint;

    void Start()
    {
        startingHealth = health;
        targetPoint = GameObject.FindGameObjectWithTag("Player");
        startingPoint = this.transform.position;

        if(!agent)
        {
            Debug.LogError("Navmesh Not found!");
            return;
        }
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!targetPoint)
            return;
        
        if(!agent.isOnNavMesh)
        {
            Debug.LogError("NavMesh Not found!");
            return;
        }

        agent.SetDestination(targetPoint.transform.position);
        Vector2 direction = targetPoint.transform.position - transform.position;

        // Calculate the angle (in degrees) and apply it
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void TakeDamage(uint new_damage_amt)
    {
        if(health - new_damage_amt <= 0)
        {
            ModifyItsHealthBar();
            OnDeath();
        }
        
        health -= new_damage_amt;
        ModifyItsHealthBar();
    }

    public virtual void OnDeath()
    {
        uint totalCoinsEnemyLost = deathGold + goldStealAmount;
        GameManager.player.AddGold((uint)(deathGold * GoldBoost));
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
        if(collision.gameObject.tag == "Player")
        {
            // OnStealGold(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GoldPile")
        {
            OnStealGold(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GoldPile")
        {
            OnStealGold(this.gameObject);
        }
    }

    void ModifyItsHealthBar()
    {
        // HealthBar mod.
        float percentage = (float)(health / startingHealth);
        Vector3 newScale = new Vector3(percentage, .2f, 1);
        healthBar.transform.localScale = newScale;
    }
}
