using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class WaveManager : MonoBehaviour
{
    [Range(0, 50)]
    [SerializeField] uint numberOfWaves;

    [Range(0, 240f)]
    public float timeLimitPerWave;
    float currentTime;

    int currentWaveCount;

    [SerializeField] TMP_Text waveTracker;
    [SerializeField] TMP_Text timeTracker;

    System.Random rnd;

    void Start() 
    {
        ResetWaveCount();
        currentTime = timeLimitPerWave;
    }

    void Update()
    {
        if(!TimeRemaining())
        {
            MoveToNextWave();
            currentTime = timeLimitPerWave;
        }

        RunTheClock();

    }

    void UpdateText()
    {
        waveTracker.text = $"Wave #{currentWaveCount}";
    }

    void MoveToNextWave()
    {
        currentWaveCount += 1;
        UpdateText();
    }

    public int GetWaveCount()
    {
        return currentWaveCount;
    }

    public void ResetWaveCount()
    {
        currentWaveCount = 1;
        UpdateText();
    }

    void RunTheClock()
    {
        currentTime -= Time.deltaTime;
        timeTracker.text = "Time left: " + Math.Round(currentTime, 2);
    }

    public bool TimeRemaining()
    {
        return currentTime > 0;
    } 

}
