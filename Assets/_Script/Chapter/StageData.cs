using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Data/New Stage")]
public class StageData : ScriptableObject
{
    [field: SerializeField] public List<WaveData> Waves = new List<WaveData>();
    [field: SerializeField] public RewardType Type { get; private set; }
    [field: SerializeField] public List<RewardItem> Items = new List<RewardItem>();
}
