using TMPro;
using UnityEngine;

public class SlowItem : BoostItem
{
    [SerializeField] private GameObject snowStormEffect;
    new void Start()
    {
        this.Data = Player.GetInstance().GetItemData(BoostItemType.SlowDownItem);
        base.Start();
    }
    protected override void ApplyItem()
    {
        base.ApplyItem();
        snowStormEffect.SetActive(true);
        ZombieManager.GetInstance().SpeedMultiplier = Data.Value;
    }

    protected override void DiscardItem()
    {
        base.DiscardItem();
        snowStormEffect.SetActive(false);
        ZombieManager.GetInstance().SpeedMultiplier = 1f;
    }
}
