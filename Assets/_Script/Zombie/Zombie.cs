using Spine.Unity;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamageable
{
    [field: SerializeField] public ZombieData Data { get; private set; }
    public bool IsDead { get; private set; }
    public Health m_Health { get; private set; }
    public ZombieMove m_Move {get; private set; }
    public Effectable m_Effectable { get; private set; }
    public SkeletonAnimation m_SkeletonAnimation { get; private set; }
    public MeshRenderer meshRenderer { private set; get; }

    private void Awake()
    {
        m_Health = GetComponent<Health>();
        m_Move = GetComponent<ZombieMove>();
        m_Effectable = GetComponent<Effectable>();
        m_SkeletonAnimation = GetComponent<SkeletonAnimation>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        IsDead = false;
        m_Health.Reborn();
        m_Health.Register(EventID.HealthEventID.OnDie, OnDead);
        transform.localScale = new Vector3(1f, 1f, 1f); // flip
        if(Data != null) Data.CurrentMoveSpeed = Data.MoveSpeedOrigin;
    }

    private void OnDisable()
    {
        m_Health.Unregister(EventID.HealthEventID.OnDie, OnDead);
    }

    public bool InitWithData(ZombieData data)
    {
        this.Data = data;
        this.Data.TagInPool = PoolTag.ZOMBIE_TAG;
        m_SkeletonAnimation.skeletonDataAsset = data.skeletonDataAsset;
        m_SkeletonAnimation.Initialize(true);
        m_Health.SetMaxHP(Data.MaxHPOrigin);
        return true;
    }

    void OnDead(int val)
    {
        IsDead = true;
        this.MoveToPool();
        ZombieManager.GetInstance().OutGarden(this);

        // effect
        var ef = ObjectPooler.Instance.GetObject("Smoke");
        ef.transform.position = transform.position;
        ef.SetActive(true);
    }

    public bool MoveToPool()
    {
        return ObjectPooler.Instance.PutInPool(this.Data.TagInPool, gameObject);
    }

    public void TakeDamage(DamageMessage message)
    {
        if (message.EfData.Effect != EffectType.Non) 
            m_Effectable.TakeEffect(message);
        m_Health.TakeDamage(message.ATK);
    }
}
