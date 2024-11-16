using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [Range(0, 200f)]
    [SerializeField] float travelSpeed;
    [Range(0, 10f)]
    [SerializeField] uint damage;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(!rb)
        {
            Debug.Log("Rigidbody is not attached!");
            return;
        }
        rb.AddForce(this.transform.right * travelSpeed);
    }

    public void OnEnemyCollision() // Death logic for enemy
    {
        GameManager.enemy.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            OnEnemyCollision();
        }

        Destroy(this.gameObject);
    }
}
