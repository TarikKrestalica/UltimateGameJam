using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    [Range(0, 40f)]
    [SerializeField] private float rotSpeed;

    private bool hasClicked = false;
    private Vector3 offset; 

    void Update()
    {
        if (!hasClicked)
            return;

        // Handle rotation
        Vector3 curRot = this.transform.localEulerAngles;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            curRot.z -= Time.deltaTime * rotSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            curRot.z += Time.deltaTime * rotSpeed;
        }

        this.transform.localEulerAngles = curRot;
    }

    public void OnBeginDrag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector3 objectScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(pointerData.position.x, pointerData.position.y, objectScreenPosition.z));
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(pointerData.position.x, pointerData.position.y, Camera.main.WorldToScreenPoint(this.transform.position).z));
        this.transform.position = pointerWorldPosition + offset;
    }

    public void ModifyClicked()
    {
        hasClicked = !hasClicked;
    }
}