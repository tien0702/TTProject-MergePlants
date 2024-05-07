using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(menuName = "Data/Flower", fileName = "New Flower Data")]
public class FlowerData : ScriptableObject, IObjectPooler
{
    // Flower info
    public float AttackRange = 3f;
    [field: Header("Flower Attribute")]
    [field: SerializeField] public string TagInPool { get; set; }
    [field: SerializeField] public bool Unlocked { set; get; }
    [field: SerializeField] public int Level { private set; get; }
    [field: SerializeField] public float AttackSpeed { set; get; }
    [field: SerializeField] public Sprite Portrait { set; get; }
    [field: SerializeField] public SkeletonDataAsset skeletonDataAsset { set; get; }

    // Attack info
    [field: Header("Bullet Attribute")]
    [field: SerializeField] public int ATK { set; get; }
    [field: SerializeField] public Ability Ability { private set; get; }
    [field: SerializeField] public FlowerPrice Price { set; get; }
    [field: SerializeField] public BulletData BulletData { set; get; }
}
