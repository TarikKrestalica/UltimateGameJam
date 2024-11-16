using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [Range(0, 1000f)]
    [SerializeField] float travelSpeed;
    [Range(0, 100f)]
    [SerializeField] uint damage;

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
    }

    public void OnEnemyCollision(Enemy enemy) // Death logic for enemy
    {
        enemy.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Enemy e = collider.gameObject.GetComponent<Enemy>();
            OnEnemyCollision(e);
        }

        Destroy(this.gameObject);
    }
}
