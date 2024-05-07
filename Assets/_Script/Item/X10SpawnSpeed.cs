using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class X10SpawnSpeed : BoostItem
{
    [SerializeField] private FlowerSpawner spawner;

    [SerializeField] private Image circleFill;

    new void Start()
    {
        this.Data = Player.GetInstance().GetItemData(BoostItemType.X10SpeedItem);
        base.Start();
        spawner = GameObject.Find("FlowerSpawner").GetComponent<FlowerSpawner>();
    }

    new void Update()
    {
        base.Update();
        if (Data.Duration < 0) return;
        circleFill.fillAmount = spawner.ElapsedTime / spawner.TimeSpawnFlower;
    }

    protected override void ApplyItem()
    {
        base.ApplyItem();
        spawner.TimeSpawnFlower /= Data.Value;
    }

    protected override void DiscardItem()
    {
        base.DiscardItem();
        spawner.TimeSpawnFlower *= Data.Value;
    }
}
