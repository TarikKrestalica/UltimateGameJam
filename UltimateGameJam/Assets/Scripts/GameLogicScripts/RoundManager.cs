using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class RoundManager : MonoBehaviour
{
    private Animator m_animator;

    [SerializeField] TMP_Text roundTracker;
    private int curRound = 1;
    private int prevRound = 0;
    [SerializeField] AnimationClip roundTxtAppearAnim;
    [SerializeField] AnimationClip roundTxtLeaveAnim;
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        if(m_animator)
        {
            Debug.LogError("No animator present.");
        }
    }

    void Update()
    {
        if(!GameManager.waveManager.TimeRemaining())
        {
            roundTracker.text = $"Wave {GameManager.waveManager.GetWaveCount().ToString()}";
            ControlAnimation();
        }
    }

    // Round shift animation;
    async void ControlAnimation()
    {
        m_animator.SetBool("WaveOver", true);
        await Task.Delay((int)(GameManager.waveManager.TimeDelayPerWave - 1.5f) * 1000);
        m_animator.SetBool("WaveOver", false);

    }
}
