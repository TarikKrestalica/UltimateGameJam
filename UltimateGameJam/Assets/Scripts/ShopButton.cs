using UnityEngine;
using UnityEngine.Serialization;

public class ShopButton : MonoBehaviour
{
    [FormerlySerializedAs("goldAmount")] [SerializeField] uint cost;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnClick()
    {
        if (player.GoldAmount < cost)
            return;
        player.GoldAmount -= cost;
    }
}
