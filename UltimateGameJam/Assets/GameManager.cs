using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Design Pattern: Need to implement
    public static GameManager gameManager;

    public static Player player{
        get{
            if(gameManager.m_player == null)
            {
                gameManager.m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            return gameManager.m_player;
        }
    }

    private Player m_player;

    public static Enemy enemy{
        get{
            if(gameManager.m_enemy == null)
            {
                gameManager.m_enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
            }

            return gameManager.m_enemy;
        }
    }

    private Enemy m_enemy;

    public static GameObject projectile{
        get{
            if(gameManager.m_projectile == null)
            {
                gameManager.m_projectile = Resources.Load("Prefabs/BaseProjectile") as GameObject;
            }

            return gameManager.m_projectile;
        }
    }

    private GameObject m_projectile;

    void Start()
    {
        if(gameManager != null)
        {
            Destroy(gameManager);
        }
        gameManager = this;
        DontDestroyOnLoad(gameManager);
    }
}
