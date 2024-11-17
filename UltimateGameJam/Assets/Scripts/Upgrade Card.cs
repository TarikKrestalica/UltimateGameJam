using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text currentValueText;
    [SerializeField] TMP_Text nextValueText;

    [SerializeField] int level;
    [SerializeField] int cost;
    [SerializeField] int currentValue;
    [SerializeField] int nextValue;

    public void UpdateUI()
    {
        levelText.text = $"Level {level}";
        costText.text = $"${cost}";
        currentValueText.text = currentValue.ToString("0.00");
        nextValueText.text = nextValue.ToString("0.00");
    }

    public void OnUpgradeClicked()
    {
        // Logic for upgrading, e.g., checking player currency and applying the upgrade
    }
}
