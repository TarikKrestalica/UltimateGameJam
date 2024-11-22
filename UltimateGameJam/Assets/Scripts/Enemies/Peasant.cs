using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : Enemy
{
    [Range(0, 5f)]
    [SerializeField] private float goldStealRate;

    private GameObject collisionObject;
    float curRate = 0f;

    bool hasEnteredPile = false;

    // Start is called before the first frame update
    public override void Start()
    {
        startingHealth = health;
        targetPoint = GameObject.FindGameObjectWithTag("Target");
        collisionObject = null;
        if(!agent)
        {
            Debug.LogError("Navmesh Not found!");
            return;
        }
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
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

        if(hasEnteredPile) // Peasant will slowly take little bits of gold at a time.
        {
            if(curRate <= 0) 
            {
                InitiateSteal(collisionObject);
            }

            curRate -= Time.deltaTime;
        }
    }

    public override void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "GoldPile")
        {
            hasEnteredPile = true;
            collisionObject = collider.gameObject;
            InitiateSteal(collisionObject);
        }
    }

    public override void OnStealGold(GameObject g)
    {
        GameManager.player.TakeDamageToGoldStash(goldStealAmount);
    }

    void InitiateSteal(GameObject g)
    {
        this.OnStealGold(g);
        curRate = goldStealRate;
    }
}
