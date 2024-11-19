using System;
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class GoldPile : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    private void Awake() => spriteRenderer = this.Get<SpriteRenderer>();

    public void UpdateSprite(uint goldAmount)
    {
        spriteRenderer.sprite = goldAmount switch
        {
            0      => sprites[0],
            < 200  => sprites[1],
            < 800  => sprites[2],
            < 1600  => sprites[3],
            < 3200 => sprites[4],
            < 6400 => sprites[5],
            _      => sprites[6],
        };
    }
}
