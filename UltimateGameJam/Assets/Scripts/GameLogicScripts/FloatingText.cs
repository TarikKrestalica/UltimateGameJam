using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class FloatingText : MonoBehaviour
{
    public TMP_Text textMesh;                          
    public Color StartColor;    
    [SerializeField] float floatSpeed = 1f;     
    [SerializeField] float fadeDuration = 1f;   
    
    private float elapsedTime = 0f;

    private void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TMP_Text>();
            if (textMesh == null)
            {
                Debug.LogError("TMP_Text component is missing!");
                return;
            }
        }
        textMesh.material = textMesh.fontMaterial; 
        textMesh.color = StartColor;
        
        if (transform.parent == null)
        {
            var canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                transform.SetParent(canvas.transform, false);
            }
            else
            {
                Debug.LogWarning("No Canvas found in the scene!");
            }
        }
        Destroy(gameObject, fadeDuration);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        transform.Translate(floatSpeed * Time.deltaTime * Vector3.up);
        var alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
        textMesh.color = new Color(StartColor.r, StartColor.g, StartColor.b, alpha);

        if (elapsedTime >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}

