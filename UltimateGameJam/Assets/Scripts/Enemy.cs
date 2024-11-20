using System.Collections;
using System.Collections.Generic;
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
    GameObject targetPoint;

    [SerializeField] GameObject healthBar;

    [SerializeField] NavMeshAgent agent;

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
        if(!agent.isOnNavMesh)
        {
            Debug.LogError("NavMesh Not found!");
            return;
        }

        agent.SetDestination(targetPoint.transform.position);
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
            OnStealGold(this.gameObject);
        }
    }

    private void OnTriggerEnter2dD(Collider2D collision)
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
