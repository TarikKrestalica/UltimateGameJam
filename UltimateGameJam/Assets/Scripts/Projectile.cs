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

    public static int Damage = 10;

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
    private void OnCollisionEnter2D(Collision2D collider)
    {
        var e = collider.gameObject.GetComponent<Enemy>();
        if(collider.gameObject.tag == "Enemy")
        {
            e = collider.gameObject.GetComponent<Enemy>();
            e.TakeDamage(Damage);
        }
        else if(collider.gameObject.tag == "Businessman")
        {
            e = collider.gameObject.GetComponent<Businessman>();
            e.TakeDamage(Damage);
        }
        else if(collider.gameObject.tag == "CEO")
        {
            e = collider.gameObject.GetComponent<CEO>();
            e.TakeDamage(Damage);
        }
        else if(collider.gameObject.tag == "Politician")
        {
            e = collider.gameObject.GetComponent<Politician>();
            e.TakeDamage(Damage);
        }
        else if(collider.gameObject.tag == "Peasant")
        {
            e = collider.gameObject.GetComponent<Peasant>();
            e.TakeDamage(Damage);
        }

        Destroy(this.gameObject);

    }
}
