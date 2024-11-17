using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public uint GoldAmount
    {
        get => goldAmount;
        set => goldAmount = value;
    }
    public float ReloadTime = 0.5f;
    
    [SerializeField] uint goldAmount;
    [SerializeField] TMP_Text goldAmountTxt;
    
    [Range(0, 100f)]
    [SerializeField] float rotationSpeed;
    [SerializeField] GoldPile goldPile;
    [SerializeField] GameObject equippedWeapon;
    
    private Camera mainCamera;
    private float lastFireTime = 0f;

    private void Start()
    {
        SetCurrentGoldAmount(goldAmount);
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Vector3 curRotation = this.transform.localEulerAngles;
        
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

            Debug.Log("Fire bullet!");
        }
    }

    private void OnDeath() 
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
        if (goldAmount - amt <= 0)
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
