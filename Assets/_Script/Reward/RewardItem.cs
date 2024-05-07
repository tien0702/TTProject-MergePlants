using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardItem
{
    [field: SerializeField] public BoostItemType Type{ private set; get; }
    [field: SerializeField] public int Amount{ private set; get; }
    [field: SerializeField] public int Price { private set; get; }
}
