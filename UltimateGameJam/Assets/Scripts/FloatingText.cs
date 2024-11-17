using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;     
    [SerializeField] private float fadeDuration = 1f;   
    public TMP_Text textMesh;                          
    public Color StartColor;                           

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        textMesh.color = StartColor;
        Destroy(gameObject, fadeDuration);
    }

    private void Update()
    {
        transform.Translate(floatSpeed * Time.deltaTime * Vector3.up);
        float alpha = Mathf.Lerp(1f, 0f, Time.time / fadeDuration);
        textMesh.color = new Color(StartColor.r, StartColor.g, StartColor.b, alpha);
    }
}

