using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    [field: SerializeField] public List<RewardItem> Items = new List<RewardItem>();
    [field: SerializeField] public ZombieData[] Zombies { private set; get; }
}
