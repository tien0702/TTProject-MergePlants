using UnityEngine;
using Spine.Unity;
using DG.Tweening;

public enum FlowerState
{
    FlowerIdle,
    FlowerAttack
}

public class Flower : MonoBehaviour
{
    [field: Header("Components")]
    [field: SerializeField] public SkeletonAnimation skeletonAnimation { private set; get; }
    [field: SerializeField] private FlowerAttack flowerAttack;
    [field: SerializeField] private BoxCollider2D boxCollider2D;
    [field: Header("Data")]
    [field: SerializeField] public FlowerData Data { private set; get; }
    public static bool IsAngry { private set; get; }
    public FlowerState State { get; private set; }

    [field: Header("Effects")]
    [SerializeField] private GameObject angryEffect;

    private void Update()
    {
        if (State == FlowerState.FlowerAttack) return;
        var target = DetectZombie.DetectNearest(transform, Data.AttackRange);
        if (target == null) return;
        State = FlowerState.FlowerAttack;
        target.m_Health.Register(EventID.HealthEventID.OnDie, TargetDead);
        flowerAttack.BeginAttack(target);
    }
    private void OnEnable()
    {
        angryEffect.SetActive(Flower.IsAngry);
        float speed = FlowerManager.GetInstance().ATKSpeedMultiplier;
        skeletonAnimation.state.GetCurrent(0).TimeScale = speed;
        FlowerManager.GetInstance().OnSpeedMultipChange += OnATKSpeedChange;
    }

    private void OnDisable()
    {
        FlowerManager.GetInstance().OnSpeedMultipChange -= OnATKSpeedChange;
    }

    public bool InitWithData(FlowerData data)
    {
        this.Data = data;
        this.Data.TagInPool = "Flower";
        skeletonAnimation.skeletonDataAsset = data.skeletonDataAsset;
        skeletonAnimation.Initialize(true);
        return true;
    }


    public void SetAngry(bool angry)
    {
        Flower.IsAngry = angry;
        angryEffect.SetActive(angry);
    }

    void TargetDead(int val)
    {
        float speed = FlowerManager.GetInstance().ATKSpeedMultiplier;
        skeletonAnimation.state.SetAnimation(0, "idle", true).TimeScale = speed;
        State = FlowerState.FlowerIdle;
    }

    public bool MoveToPool()
    {
        return ObjectPooler.Instance.PutInPool(this.Data.TagInPool, gameObject);
    }

    public void MoveLocalToOrigin(float time)
    {
        transform.DOLocalMove(Vector3.zero, time);
    }

    public void SetBoxColliderActive(bool active)
    {
        boxCollider2D.enabled = active;
    }

    void OnATKSpeedChange()
    {
        float speed = FlowerManager.GetInstance().ATKSpeedMultiplier;

        if (this.State == FlowerState.FlowerAttack) speed *= this.Data.AttackSpeed;
        this.skeletonAnimation.state.GetCurrent(0).TimeScale = speed;
    }
}
