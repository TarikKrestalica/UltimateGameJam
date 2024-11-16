using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class Player : MonoBehaviour
{
    [SerializeField] private uint goldAmount;

    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite playerSprite;

    private void Awake()
    {
        spriteRenderer = this.Get<SpriteRenderer>();

        if (playerSprite != null)
            spriteRenderer.sprite = playerSprite;
    }

    public virtual void OnDeath() {  }

    // Method to set the sprite dynamically (optional)
    public void SetPlayerSprite(Sprite newSprite)
    {
        playerSprite = newSprite;

        if (spriteRenderer != null)
            spriteRenderer.sprite = playerSprite;
    }
}