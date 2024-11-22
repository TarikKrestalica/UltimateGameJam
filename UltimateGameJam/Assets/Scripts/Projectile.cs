using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    
    [Range(0, 1000f)]
    [SerializeField] float travelSpeed;

    public static uint Damage = 10;

    private float startTime = 0;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.Log("Rigidbody is not attached!");
            return;
        }
        rb.AddForce(this.transform.right * travelSpeed);
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime >= 5.0)
            this.Destroy();
    }

    public void OnEnemyCollision(Enemy enemy) // Death logic for enemy
    {
        enemy.TakeDamage(Damage);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        Enemy e = collider.gameObject.GetComponent<Enemy>();
        OnEnemyCollision(e);
        Destroy(this.gameObject);

    }
}
