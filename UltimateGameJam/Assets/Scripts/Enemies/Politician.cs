using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The damage done will depend on player proximity.
public class Politician : Enemy
{
    private float damageFalloffDistance = 10f; // Distance at which damage reduces to near zero

    public override void Update()
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

    public void ComputeAndApplyDamage()
    {
        Vector3 playerLocation = Camera.main.ScreenToWorldPoint(GameManager.player.transform.position);
        playerLocation.z = 0;

        float distance = Vector3.Distance(this.transform.position, GameManager.player.transform.position);
        Debug.Log("Distance: " + distance);
        float damage = Mathf.Clamp(goldStealAmount * distance, 0f, goldStealAmount);
        Debug.Log("Damage: " + damage);

        // Apply damage to the player
        GameManager.player.TakeDamageToGoldStash((uint)damage);
    }

    public override void TakeDamage(uint new_damage_amt)
    {
        if(health - new_damage_amt <= 0)
        {
            ModifyItsHealthBar();
            ComputeAndApplyDamage();
            OnDeath();
        }
        
        health -= new_damage_amt;
        ModifyItsHealthBar();
    }

    
}
