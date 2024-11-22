using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using ExtensionMethods;

public class Item : MonoBehaviour
{
    [Range(0, 40f)]
    [SerializeField] float rotSpeed;

    [SerializeField] GameObject healthBar;
    [Range(0, 100f)]
    [SerializeField] private float health;

    [SerializeField] GameObject card;
    [SerializeField] TMP_Text healthTxt;

    float currentHealth;

    void Awake()
    {
        UpgradeCard cref = GameObject.FindGameObjectWithTag("Upgrade").GetComponent<UpgradeCard>();
        health = cref.GetCurrentValue();
        currentHealth = health;
        healthTxt.text = $"{currentHealth}";
    }

    bool hasClicked = false;
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        if(!hasClicked)
            return;
            
        Vector3 curRot = this.transform.localEulerAngles;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            curRot.z -= Time.deltaTime * rotSpeed;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            curRot.z += Time.deltaTime * rotSpeed;
        }

        this.transform.localEulerAngles = curRot;
    }
    public void OnDrag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position = pointerData.position;
        this.transform.localPosition = position;
    }

    public void ModifyClicked()
    {
        hasClicked = !hasClicked;
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Projectile")
        {
            currentHealth -= Projectile.Damage;
            healthTxt.text = $"{currentHealth}";
        }
    }

    public void UpdateHealth(float multiplier)
    {
        health *= (health * multiplier);
    }
}
