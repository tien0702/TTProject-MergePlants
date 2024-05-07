using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoostItemType
{
    X2CoinItem,
    X10SpeedItem,
    AngryItem,
    SlowDownItem,
    AutoMergeItem
}

[System.Serializable]
public class BoostItemData
{
    [field: SerializeField] public BoostItemType Type { set; get; }
    [field: SerializeField] public float Duration { set; get; }
    [field: SerializeField] public float MaxDuration { set; get; }
    [field: SerializeField] public int Quantity { set; get; }
    [field: SerializeField] public int Price { set; get; }
    [field: SerializeField] public int MoreTime { set; get; }
    [field: SerializeField] public float Value { set; get; }
}
