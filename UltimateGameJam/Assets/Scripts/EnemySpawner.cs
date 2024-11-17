using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;   
    [SerializeField] float minRadius = 7f;     
    [SerializeField] float maxRadius = 15f;     
    [SerializeField] int numberOfEnemies = 10; 
    [SerializeField] Transform playerTransform;

    private int enemySpawnRate = 2;


    private void Start()
    {
        SpawnEnemiesAroundPlayer();
    }

    void Update()
    {
        if (!GameManager.waveManager.TimeRemaining())
        {
            AdjustSpawningParameters(GameManager.waveManager.GetWaveCount());
            SpawnEnemiesAroundPlayer();
        }
    }

    void SpawnEnemiesAroundPlayer()
    {
        for (int i = 0; i < enemySpawnRate; i++)
        {
            Vector3 spawnPosition = GetRandomPositionInRing();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionInRing()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float radius = Random.Range(minRadius, maxRadius);

        return new Vector3(
            playerTransform.position.x + Mathf.Cos(angle) * radius,
            playerTransform.position.y + Mathf.Sin(angle) * radius,
            playerTransform.position.z
        );
    }

    void AdjustSpawningParameters(int waveCount)
    {
        enemySpawnRate += enemySpawnRate / 8 + 1;
    }
}