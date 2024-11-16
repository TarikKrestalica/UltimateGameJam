using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    
    [Range(0, 1000f)]
    [SerializeField] float travelSpeed;

    [SerializeField] public uint Damage { get; set; } = 10;

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

    public void OnEnemyCollision() // Death logic for enemy
    {
        GameManager.enemy.TakeDamage(Damage);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            OnEnemyCollision();
        }

        Destroy(this.gameObject);
    }
}
