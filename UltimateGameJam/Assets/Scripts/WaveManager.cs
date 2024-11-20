using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using System.Threading.Tasks;
using ExtensionMethods;
using UnityEditor;

public class WaveManager : MonoBehaviour
{
    [Range(0, 50)]
    [SerializeField] uint numberOfWaves;

    [Range(0, 240f)]
    [SerializeField] float timeLimitPerWave;

    [Range(0, 10f)]
    [SerializeField] uint timeDelayPerWave;

    public uint TimeDelayPerWave
    {
        get => timeDelayPerWave;
        set => timeDelayPerWave = value;
    }

    float currentTime;

    int currentWaveCount = 0;

    [SerializeField] TMP_Text waveTracker;
    [SerializeField] TMP_Text timeTracker;

    bool hasTransitionExecuted = false;

    System.Random rnd;

    void Start() 
    {
        StartCoroutine(SetUpWaveControl());
    }

    void Update()
    {
        if(TimeRemaining())
        {
            if(hasTransitionExecuted)
            {
                hasTransitionExecuted = false;
            }
            RunTheClock();
        }
    }

    void UpdateText()
    {
        waveTracker.text = currentWaveCount.ToString();
    }

    void StopCurrentWave()
    {
        currentTime = 0;
        // waveTracker.gameObject.SetActive(false);
        UpdateText();
    }

    void MoveToNextWave()
    {
        currentWaveCount += 1;
        UpdateText();
        currentTime = timeLimitPerWave;
        waveTracker.gameObject.SetActive(true);
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
        currentTime.Log();
        timeTracker.text = Math.Round(currentTime, 2).ToString("00.00");
    }

    public bool TimeRemaining()
    {
        return currentTime > 0;
    } 

    void SetUpTheNextWave()
    {
        Debug.Log("Next Wave!");
        StartWave();
    }

    public void StartWave()
    {
        GameManager.enemyManager.SpawnEnemiesAroundPlayer();
        MoveToNextWave();
        Debug.Log("New wave has begun!");
    }

    async void MakeTransition()
    {
        StopCurrentWave();
        await Task.Delay((int)timeDelayPerWave * 1000);
        SetUpTheNextWave();
        hasTransitionExecuted = true;
    }

    public void SetTransitionBool(bool toggle)
    {
        hasTransitionExecuted = toggle;
    }

    public bool TransitionBool()
    {
       return hasTransitionExecuted;
    }

    public IEnumerator SetUpWaveControl()
    {
        if(!TimeRemaining())
        {
            if(!hasTransitionExecuted){
                MakeTransition(); 
                hasTransitionExecuted = true;
            }      
        }        

        StopAllCoroutines();
        yield return null;
    }
}
