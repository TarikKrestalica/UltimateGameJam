using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class GoldPile : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    private CircleCollider2D collider2D;

    private void Awake() => (spriteRenderer, collider2D) = (this.Get<SpriteRenderer>(), this.Get<CircleCollider2D>());

    public void UpdateSprite(uint goldAmount)
    {
        (spriteRenderer.sprite, collider2D.radius) = goldAmount switch
        {
            0      => (sprites[0], 0),
            < 100  => (sprites[1], 0.5f),
            < 400  => (sprites[2], 0.6f),
            < 800  => (sprites[3], 0.8f),
            < 1600 => (sprites[4], 1.05f),      
            < 3200 => (sprites[5], 1.35f),
            _      => (sprites[6], 1.75f),
        };
    }
}
