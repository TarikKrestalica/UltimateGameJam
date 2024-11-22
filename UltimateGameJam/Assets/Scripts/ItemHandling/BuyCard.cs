using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyCard : MonoBehaviour
{
    [SerializeField] TMP_Text costText;
    [SerializeField] int cost;

    [SerializeField] GameObject item;
    [SerializeField] string itemName;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void OnUpgradeClicked()
    {
        if (GameManager.player.GoldAmount < cost)
            return;
            
        GameManager.player.GoldAmount -= cost;
        GameManager.player.SetCurrentGoldAmount( GameManager.player.GoldAmount);
        CreateObject(); 
        UpdateUI();
    }

    public void UpdateUI()
    {
        costText.text = $"${cost}";
    }

    void CreateObject()
    {
        GameObject newItem = Instantiate(LoadNeededItem(), Vector3.up, Quaternion.identity, GameObject.FindWithTag("Background").transform);
    }

    GameObject LoadNeededItem()
    {
        string path = $"Prefabs/{itemName}";
        GameObject dup = Resources.Load(path) as GameObject;
        return dup;
    }

}
