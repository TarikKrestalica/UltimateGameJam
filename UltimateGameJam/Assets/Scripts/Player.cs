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
        Debug.Log("Game Over Logic here!");
    }

    void SetCurrentGoldAmount(uint goldAmt)
    {
        goldAmountTxt.text = $"Coins: {goldAmount}";
    }

    // Enemy collision.
    public void TakeDamageToGoldStash(uint amt)
    {
        if(goldAmount - amt <= 0)
        {
            OnDeath();
        }

        goldAmount -= amt;
        SetCurrentGoldAmount(goldAmount);
    }
}
