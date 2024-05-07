using TMPro;
using UnityEngine;

public class AutoMergeItem : BoostItem
{
    new void Start()
    {
        this.Data = Player.GetInstance().GetItemData(BoostItemType.AutoMergeItem);
        base.Start();
    }
    protected override void ApplyItem()
    {
        base.ApplyItem();
        GardenGrid.Instance.AutoMerge = true;
    }

    protected override void DiscardItem()
    {
        base.DiscardItem();
        GardenGrid.Instance.AutoMerge = false;
    }
}
