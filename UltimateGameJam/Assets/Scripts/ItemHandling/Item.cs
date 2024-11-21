using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Item : MonoBehaviour
{
    [Range(0, 40f)]
    [SerializeField] private float rotSpeed;

    private bool isMouseOver = false;
    private bool isDragging = false;
    private Vector3 offset;
    private Collider2D collider;

    private NavigationBaker navMeshBaker; // Reference to NavigationBaker

    private void Start() {
        collider = GetComponent<Collider2D>();
        navMeshBaker = FindObjectOfType<NavigationBaker>();
    }

    private void Update()
    {
        DetectMouseOver();

        if (Input.GetMouseButtonDown(0) && isMouseOver)
            OnMouseDown();
        if (Input.GetMouseButtonUp(0) && isDragging)
            OnMouseUp();
        if (isDragging)
            HandleRotation();
    }

    private void DetectMouseOver()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ignore Z-axis for 2D

        isMouseOver = collider.bounds.Contains(mousePosition);
    }

    private void OnMouseDown()
    {
        (isDragging, collider.enabled) = (true, false);

        // Calculate offset
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        offset = transform.position - mousePosition;
    }

    private void OnMouseUp()
    {
        (isDragging, collider.enabled) = (false, true);
    }

    private void HandleRotation()
    {
        Vector3 curRot = transform.localEulerAngles;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            curRot.z -= Time.deltaTime * rotSpeed;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            curRot.z += Time.deltaTime * rotSpeed;

        transform.localEulerAngles = curRot;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) 
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Maintain original Z position
        transform.position = mousePosition + offset;
    }
}
