using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using TMPro;
using UnityEngine;

enum UpgradeType
{
    Damage, Reload, GoldBoost
}

public class UpgradeCard : MonoBehaviour
{
    #region Variables
    [Header("Text Components")]
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text currentValueText;
    [SerializeField] TMP_Text nextValueText;

    [Space]
    [Header("Upgrade Data")]
    [SerializeField] int level;
    [SerializeField] int cost;
    [SerializeField] float currentValue;
    [SerializeField] float maxValue;
    [SerializeField] UpgradeType upgradeType;

    [Space]
    [Header("Upgrade Multipliers")]
    [Range(0, 2f)]
    [SerializeField] float costMultiplier = 1.75f;
    [Range(0, 2f)]
    [SerializeField] float nextValueMultiplier = 1.5f;

    [Space]
    [Header("Formatting Options")]
    [SerializeField] bool useDecimals = false;
    [SerializeField] string suffix = "";
    [SerializeField] string prefix = "";
    #endregion
    
    [SerializeField] private float nextValue;

    private void Start()
    {
        nextValue = currentValue * nextValueMultiplier;
        UpdateUI();
    }

    public void UpdateUI()
    {
        levelText.text = $"Level {level}";
        costText.text = $"${cost}";
        currentValueText.text = FormatValue(currentValue);
        nextValueText.text = FormatValue(nextValue);
    }
    
    
    private string FormatValue(float value) => $"{prefix}{value.ToString(useDecimals ? "0.00" : "0")}{suffix}";

    private float GetNewCost() => cost * costMultiplier;

    public void OnUpgradeClicked()
    {
        if (GameManager.player.GoldAmount < cost)
            return;
            
        GameManager.player.GoldAmount -= cost;
        GameManager.player.SetCurrentGoldAmount( GameManager.player.GoldAmount);
        
        level++;
        
        currentValue = nextValue;
        nextValue = currentValue * nextValueMultiplier;
        cost = (int)GetNewCost();

        switch (upgradeType)
        {
            case UpgradeType.Damage:
                Projectile.Damage = (int)nextValue;
                break;
            case UpgradeType.Reload:
                GameManager.player.ReloadTime = nextValue;
                break;
            case UpgradeType.GoldBoost:
                Enemy.GoldBoost = nextValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        UpdateUI();
    }
}
