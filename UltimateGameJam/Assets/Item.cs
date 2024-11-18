using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    [Range(0, 40f)]
    [SerializeField] float rotSpeed;

    bool hasClicked = false;
    void Update()
    {
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


}
