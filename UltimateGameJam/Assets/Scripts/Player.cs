using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] uint goldAmount;
    [SerializeField] TMP_Text goldAmountTxt;
    
    [Range(0, 100f)]
    [SerializeField] float rotationSpeed;
    [SerializeField] GoldPile goldPile;
    // [SerializeField] Weapon equippedWeapon;

    void Start()
    {
        SetCurrentGoldAmount(goldAmount);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curRotation = this.transform.localEulerAngles;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            curRotation.z -= rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            curRotation.z += rotationSpeed * Time.deltaTime;
        }
        
        // TODO:DELETE
        SetCurrentGoldAmount(goldAmount);

        this.transform.localEulerAngles = curRotation;

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fire bullet!");
            Instantiate(GameManager.projectile, GameManager.player.transform.position + GameManager.player.transform.right * 1.2f, GameManager.player.transform.rotation);
        }
    }

    public virtual void OnDeath() 
    {
        Debug.Log("Game Over Logic here!");
    }

    void SetCurrentGoldAmount(uint goldAmt)
    {
        goldAmountTxt.text = $"Coins: {goldAmount}";
        goldPile.UpdateSprite(goldAmt);
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

    public void AddGold(uint amount)
    {
        goldAmount += amount;
        SetCurrentGoldAmount(goldAmount);
    }
}
