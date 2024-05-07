using Spine;
using Spine.Unity;
using UnityEngine;

public class FlowerAttack : MonoBehaviour
{
    Zombie target;
    Vector3 shootPointPosition;

    // Components
    Flower flower;
    BulletData bulletData;
    SkeletonAnimation m_SkeletonAnimation;

    private void OnEnable()
    {
        flower = GetComponent<Flower>();
        if (flower.Data == null) return;
        bulletData = flower.Data.BulletData;
        bulletData.Attack = flower.Data.ATK;
        bulletData.Ability = flower.Data.Ability;

        m_SkeletonAnimation = GetComponent<SkeletonAnimation>();
        m_SkeletonAnimation.AnimationState.Event += HandleEventTriggered;

        var shootBone = m_SkeletonAnimation.skeleton.FindBone("shoot-point");
        shootPointPosition = new Vector2(shootBone.WorldX, shootBone.WorldY);
    }

    void Attack(Zombie target)
    {
        var direction = target.transform.position - transform.position;
        if (direction.x != 0) Flip(direction);

        var bulletObj = ObjectPooler.Instance.GetObject(bulletData.TagInPool);
        if (bulletObj == null) return;
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.InitWithData(bulletData);

        Vector3 shootPos = shootPointPosition;
        shootPos.x *= transform.localScale.x;
        var pos = transform.position + shootPos;
        bullet.transform.position = pos;
        bullet.Launch(target);
    }

    void Flip(Vector2 direction)
    {
        int val = (direction.x < 0) ? (-1) : (1);
        transform.localScale = new Vector3(val, 1, 1);
    }

    private void HandleEventTriggered(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == EventID.SkelAniID.ATTACK_EVENT)
        {
            this.Attack(this.target);
        }
    }

    public void BeginAttack(Zombie target)
    {
        var direction = target.transform.position - transform.position;
        if (direction.x != 0) Flip(direction);
        float speed = FlowerManager.GetInstance().ATKSpeedMultiplier * flower.Data.AttackSpeed;
        m_SkeletonAnimation.state.SetAnimation(0, "attack", true).TimeScale = speed;
        this.target = target;
    }
}
