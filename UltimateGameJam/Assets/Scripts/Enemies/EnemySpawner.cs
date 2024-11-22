using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;   
    [SerializeField] float minRadius = 3f;     
    [SerializeField] float maxRadius = 8f;     
    [SerializeField] int numberOfEnemies = 10; 
    [SerializeField] Transform playerTransform;

    [Range(0, 10f)]
    [SerializeField] float timeDelay;

    float curDelay;

    [Range(1, 10)]
    [SerializeField] int enemySpawnRate; 

    System.Random rnd;

    void Start()
    {
        curDelay = timeDelay;
        rnd = new System.Random();
    } 

    void Update()
    {   
        if(!GameManager.waveManager.TimeRemaining())
        {
            if(!GameManager.waveManager.TransitionBool())
            {
                StartCoroutine(GameManager.waveManager.SetUpWaveControl());
            }
            curDelay = 0;
            return;
        }

        if(curDelay >= timeDelay)
        {
            SpawnEnemiesAroundPlayer();
            curDelay = 0;
        }

        curDelay += Time.deltaTime;
    }

    public void SpawnEnemiesAroundPlayer()
    {
        for (int i = 0; i < enemySpawnRate; i++)
        {
            Vector3 spawnPosition = GetRandomPositionInRing();
            Instantiate(ChooseEnemy(), spawnPosition, Quaternion.identity);
        }  
    }

    Vector3 GetRandomPositionInRing()
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
        float radius = UnityEngine.Random.Range(minRadius, maxRadius);

        return new Vector3(
            playerTransform.position.x + Mathf.Cos(angle) * radius,
            playerTransform.position.y + Mathf.Sin(angle) * radius,
            playerTransform.position.z
        );
    }

    public void AdjustSpawningParameters(int waveCount)
    {
        switch(waveCount)
        {
            case 3:
                enemySpawnRate += 1;
                timeDelay -= .5f;
                maxRadius -= .5f;
                break;
            case 5:
                enemySpawnRate += 1;
                timeDelay -= .5f;
                maxRadius -= .25f;
                break;
            case 7:
                enemySpawnRate += 1;
                timeDelay -= .5f;
                maxRadius -= .25f;
                break;
            case 9:
                enemySpawnRate += 1;
                timeDelay -= .2f;
                maxRadius -= .25f;
                break;
            case 11:
                enemySpawnRate += 1;
                timeDelay -= .3f;
                maxRadius -= .25f;
                break;
            default:
                break;
        }
    }

    public GameObject ChooseEnemy()
    {
        return enemyPrefabs[rnd.Next(0, enemyPrefabs.Count)];
    }

    public void ResetDelay()
    {
        curDelay = timeDelay;
    }

    public List<GameObject> GetEnemies()
    {
        return enemyPrefabs;
    }
}