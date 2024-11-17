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
    [SerializeField] UpgradeType upgradeType;

    [Space]
    [Header("Upgrade Multipliers")]
    [SerializeField] float costMultiplier = 1.75f;
    [SerializeField] float nextValueMultiplier = 1.5f;

    [Space]
    [Header("Formatting Options")]
    [SerializeField] bool useDecimals = false;
    [SerializeField] string suffix = "";
    [SerializeField] string prefix = "";
    #endregion
    
    private float nextValue;
    private Player player;

    private void Start()
    {
        nextValue = GetNewNextCost();
        player = GameObject.FindWithTag("Player").Get<Player>();       
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
    private float GetNewNextCost() => currentValue * nextValueMultiplier;

    public void OnUpgradeClicked()
    {
        if (player.GoldAmount < cost)
            return;
        player.GoldAmount -= (uint)cost;
        
        level++;
        currentValue = nextValue;
        (nextValue, cost) = (GetNewNextCost(), (int)GetNewCost());

        switch (upgradeType)
        {
            case UpgradeType.Damage:
                Projectile.Damage = (uint)nextValue;
                break;
            case UpgradeType.Reload:
                player.ReloadTime = nextValue;
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
