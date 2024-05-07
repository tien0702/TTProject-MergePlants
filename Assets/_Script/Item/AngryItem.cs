using TMPro;
using UnityEngine;

public class AngryItem : BoostItem
{
    new void Start()
    {
        this.Data = Player.GetInstance().GetItemData(BoostItemType.AngryItem);
        base.Start();
    }
    protected override void ApplyItem()
    {
        base.ApplyItem();
        FlowerManager.GetInstance().ATKSpeedMultiplier = Data.Value;
        GardenGrid.Instance.GetFlowersInGarden().ForEach(f => f.SetAngry(true));
    }

    protected override void DiscardItem()
    {
        base.DiscardItem();
        FlowerManager.GetInstance().ATKSpeedMultiplier = 1f;
        GardenGrid.Instance.GetFlowersInGarden().ForEach(f => f.SetAngry(false));
    }
}
