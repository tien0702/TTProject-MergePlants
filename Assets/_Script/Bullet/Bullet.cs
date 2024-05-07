using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] public BulletData Data { get; private set; }
    Zombie target;

    // Components
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trailRenderer;

    private void Update()
    {
        if (Vector2.Distance(target.transform.position, transform.position) < 0.1f || target.IsDead)
        {
            if (!target.IsDead) this.SendDamage(target);
            Impact();
            MoveToPool();
            return;
        }
        Vector2 direction = target.transform.position - transform.position;
        Vector3 nextPos = direction.normalized * BulletData.Speed * Time.deltaTime;
        transform.position += nextPos;

        transform.right = direction;
    }

    public bool InitWithData(BulletData data)
    {
        this.Data = data;

        spriteRenderer.sprite = data.Portrait;
        trailRenderer.colorGradient = data.TrailGradient;

        return true;
    }

    public void Launch(Zombie target)
    {
        gameObject.SetActive(true);
        this.target = target;
        transform.parent = HolderController.Instance.GetByName("BulletHolder");
    }

    void SendDamage(Zombie target)
    {
        DamageMessage message = new DamageMessage(Data.Attack, Data.Ability);
        FloatingMessage.Instance.FloatingDamage(target.transform, message.ATK, message.IsCrit);
        target.TakeDamage(message);
    }

    void Impact()
    {
        var impactEf = ObjectPooler.Instance.GetObject("Impact");
        impactEf.transform.SetParent(target.transform);
        impactEf.transform.localPosition = Vector3.zero;
        impactEf.SetActive(true);
    }

    public bool MoveToPool()
    {
        return ObjectPooler.Instance.PutInPool(this.Data.TagInPool, gameObject);
    }
}
