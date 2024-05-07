using TMPro;
using UnityEngine;

public class DoubleCoinItem : BoostItem
{
    new void Start()
    {
        this.Data = Player.GetInstance().GetItemData(BoostItemType.X2CoinItem);
        base.Start();
    }
    protected override void ApplyItem()
    {
        base.ApplyItem();
        Debug.Log("X2 Coin");
    }

    protected override void DiscardItem()
    {
        base.DiscardItem();
        Debug.Log("Discard X2 Coint");
    }
}
