using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zombie Data", menuName = "Data/New Zombie Data")]
public class ZombieData : ScriptableObject, IObjectPooler
{
    [field: SerializeField] public string TagInPool { get; set; }
    [field: SerializeField] public int ID { set; get; }
    [field: SerializeField] public int Level { set; get; }
    [field: SerializeField] public int Gold { set; get; }
    [field: SerializeField] public int MaxHPOrigin { set; get; }
    [field: SerializeField] public float MoveSpeedOrigin { set; get; }
    [field: SerializeField] public float CurrentMoveSpeed { set; get; }


    [field: SerializeField] public Sprite Portrait { set; get; }
    [field: SerializeField] public SkeletonDataAsset skeletonDataAsset { set; get; }
}
