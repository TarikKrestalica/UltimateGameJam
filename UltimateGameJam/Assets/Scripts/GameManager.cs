using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public static WaveManager waveManager{
        get{
            if(gameManager.m_waveManager == null)
            {
                gameManager.m_waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
            }

            return gameManager.m_waveManager;
        }
    }

    private WaveManager m_waveManager;

    public static EnemySpawner enemyManager{
        get{
            if(gameManager.m_enemySpawner == null)
            {
                gameManager.m_enemySpawner = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemySpawner>();
            }

            return gameManager.m_enemySpawner;
        }
    }

    private EnemySpawner m_enemySpawner;

    /*
    public static NavMesh m_navMesh_Manager{
        get{
            if(gameManager.m_navMesh_Manager == null)
            {
                gameManager.m_navMesh_Manager = GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMesh>();
            }

            return gameManager.m_navMesh_Manager;
        }
    }

    private NavMesh m_navMesh_Manager;
    */

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
