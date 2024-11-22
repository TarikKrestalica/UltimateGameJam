using UnityEngine;
using UnityEngine.Serialization;

public class ShopButton : MonoBehaviour
{
    [FormerlySerializedAs("goldAmount")] [SerializeField] int cost;

    public void OnClick()
    {
        if (GameManager.player.GoldAmount < cost)
            return;
        GameManager.player.GoldAmount -= cost;
    }
}
