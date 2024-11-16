using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] uint goldAmount;
    [SerializeField] TMP_Text goldAmountTxt;
    // [SerializeField] Weapon equippedWeapon;

    void Start()
    {
        SetCurrentGoldAmount(goldAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnDeath()
    {
        
    }

    void SetCurrentGoldAmount(uint goldAmt)
    {
        goldAmount = goldAmt;
        goldAmountTxt.text = $"Coins: {goldAmount}";
    }
}
