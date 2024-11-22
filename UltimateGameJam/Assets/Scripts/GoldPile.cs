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

    public void UpdateSprite(int goldAmount)
    {
        spriteRenderer.sprite = goldAmount switch
        {
            0      => sprites[0],
            < 100  => sprites[1],
            < 400  => sprites[2],
            < 800  => sprites[3],
            < 1600 => sprites[4],
            < 3200 => sprites[5],
            _      => sprites[6],
        };
    }
}
