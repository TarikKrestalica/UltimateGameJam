using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static float GoldBoost = 1;
    protected float startingHealth;
    [SerializeField] protected float health;
    [SerializeField] protected uint goldAmount;
    [SerializeField] protected uint deathGold;
    [SerializeField] protected uint goldStealAmount;

    [SerializeField] protected GameObject targetPoint;

    [SerializeField] GameObject healthBar;

    [SerializeField] protected NavMeshAgent agent;

    private Vector3 startingPoint;

    public virtual void Start()
    {
        startingHealth = health;
        targetPoint = GameObject.FindGameObjectWithTag("Target");
        startingPoint = this.transform.position;

        if(!agent)
        {
            Debug.LogError("Navmesh Not found!");
            return;
        }
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    public virtual void Update()
    {
        if(!agent.isOnNavMesh)
        {
            Debug.LogError("NavMesh Not found!");
            return;
        }

        agent.SetDestination(targetPoint.transform.position);

        Vector3 direction = (targetPoint.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        float rotationSpeed = 200f; // Degrees per second
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(uint new_damage_amt)
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
        Destroy(this.transform.parent.gameObject);
    }

    public virtual void OnStealGold(GameObject g)
    {
        GameManager.player.TakeDamageToGoldStash(goldStealAmount);
        Destroy(g.transform.parent.gameObject); // For now.
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GoldPile")
        {
            OnStealGold(this.transform.parent.gameObject);
        }
    }

    public void ModifyItsHealthBar()
    {
        // HealthBar mod.
        float percentage = (float)(health / startingHealth);
        Vector3 newScale = new Vector3(percentage, .2f, 1);
        healthBar.transform.localScale = newScale;
    }
}
