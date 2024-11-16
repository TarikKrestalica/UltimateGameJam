using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [Range(0, 10f)]
    [SerializeField] float travelSpeed;
    [Range(0, 10f)]
    [SerializeField] int damage;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyCollision() // Death logic for enemy
    {
        GameManager.enemy.OnDeath();
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            OnEnemyCollision();
        }
    }
}
