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
    [SerializeField] protected int goldAmount;
    [SerializeField] protected int deathGold;
    [SerializeField] protected int goldStealAmount;

    [SerializeField] protected GameObject targetPoint;

    [SerializeField] GameObject healthBar;

    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected int startingWaveIndex; // Wave in which the enemy will be spawned.

    private Vector3 startingPoint;

    public virtual void Start()
    {
        if(startingWaveIndex > GameManager.waveManager.GetWaveCount()) // If it won't be introduced yet, don't spawn.
        {
            Debug.Log(this.transform.parent.name + " is not introduced yet!");
            GameManager.enemyManager.ResetDelay();
            Destroy(this.transform.parent.gameObject);
        }

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
        if(GameManager.player.GameOver())
            return;
            
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

    public virtual void TakeDamage(int new_damage_amt)
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
        int totalCoinsEnemyLost = deathGold + goldStealAmount;
        GameManager.player.AddGold(totalCoinsEnemyLost);
        Destroy(this.transform.parent.gameObject);
    }

    public virtual void OnStealGold()
    {
        GameManager.player.TakeDamageToGoldStash(goldStealAmount);
        Destroy(this.transform.parent.gameObject); // For now.
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GoldPile")
        {
            OnStealGold();
        }
    }

    public void ModifyItsHealthBar()
    {
        // HealthBar mod.
        float percentage = (float)(health / startingHealth);
        Vector3 newScale = new Vector3(percentage, .2f, 1);
        healthBar.transform.localScale = newScale;
    }

    public int WaveStart()
    {
        return startingWaveIndex;
    }
}
