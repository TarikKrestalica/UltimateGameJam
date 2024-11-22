using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public int GoldAmount
    {
        get => goldAmount;
        set => goldAmount = value;
    }
    public float ReloadTime = 0.5f;
    
    [SerializeField] int goldAmount;
    [SerializeField] TMP_Text goldAmountTxt;
    
    [Range(0, 100f)]
    [SerializeField] float rotationSpeed;
    [SerializeField] GoldPile goldPile;
    [SerializeField] GameObject equippedWeapon;

    float currentDamage;
    
    private Camera mainCamera;
    private float lastFireTime = 0f;

    private bool gameOver;

    [SerializeField] GameObject gameOverIcon;

    private void Awake()
    {
        currentDamage = 10f;
        SetCurrentGoldAmount(goldAmount);
        mainCamera = Camera.main;
        gameOverIcon.SetActive(false);
        gameOver = false;
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(goldAmount <= 0)
        {
            OnDeath();
            goldAmount = 0;
            SetCurrentGoldAmount(0);
            return;
        }
        // Converting the mouse position to a point in 3D-space
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        var direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);
        transform.right = direction;

        equippedWeapon.SetLocalPosition(this.transform.localEulerAngles);
        
        if (Time.time - lastFireTime > ReloadTime && Input.GetMouseButton(0))
        {
            lastFireTime = Time.time;
            Instantiate(
                GameManager.projectile, 
                GameManager.player.transform.position + GameManager.player.transform.right * 1.2f, 
                GameManager.player.transform.rotation);
        }
    }

    private void OnDeath() 
    {
        gameOverIcon.SetActive(true);
        gameOver = true;
    }

    public void SetCurrentGoldAmount(int goldAmt)
    {
        goldAmountTxt.text = $"{goldAmount}";
        goldPile.UpdateSprite(goldAmt);
    }

    // Enemy collision.
    public void TakeDamageToGoldStash(int amt)
    {
        if (goldAmount - amt <= 0)
        {
            OnDeath();
            goldAmount = 0;
            SetCurrentGoldAmount(0);
            return;
        }

        goldAmount -= amt;
        SetCurrentGoldAmount(goldAmount);
    }

    public void AddGold(int amount)
    {
        goldAmount += amount;
        SetCurrentGoldAmount(goldAmount);
    }

    public float GetCurrentDamage()
    {
        return currentDamage;
    }

    public void SetCurrentDamage(float newDamage)
    {
        currentDamage = newDamage;
    }

    public bool GameOver()
    {
        return gameOver;
    }
}

