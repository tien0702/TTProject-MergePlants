using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlowerDataa : IObjectPooler
{
    public float AttackRange = 3f;
    [field: SerializeField] public string TagInPool { get; set; }
    [field: SerializeField] public bool Unlocked { set; get; }
    [field: SerializeField] public int Level { private set; get; }
    [field: SerializeField] public int ATK { set; get; }
    [field: SerializeField] public Ability Ability { private set; get; }
    [field: SerializeField] public float AttackSpeed { set; get; }
    [field: SerializeField] public FlowerPrice Price { set; get; }
    [field: SerializeField] public Sprite Portrait { set; get; }
    [field: SerializeField] public BulletData BulletData { set; get; }
}
