using Spine.Unity;
using System;
using UnityEngine;

public class EffectData
{
    public EffectType Effect;
    public float Duration;
}

public class Effectable : MonoBehaviour
{
    ZombieData zombieData;
    static float EffectiveTime = 1f;
    static float CritColorTime = 0.5f;
    float slowTime, freezeTime, critColorTime;
    Color currentColor = Color.white;

    SkeletonAnimation skeletonAnimation;

    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        if (critColorTime > 0) critColorTime -= Time.deltaTime;
        if (freezeTime > 0) freezeTime -= Time.deltaTime;
        if (slowTime > 0) slowTime -= Time.deltaTime;
        RecheckSpeed();
        RecheckColor();
    }

    private void OnEnable()
    {
        zombieData = GetComponent<Zombie>().Data;
    }

    void RecheckSpeed()
    {
        float speed = zombieData.MoveSpeedOrigin;
        float factor = 1f;
        if (freezeTime > 0) factor = 0f;
        else if (slowTime > 0) factor = 0.5f;
        zombieData.CurrentMoveSpeed = speed * factor;
        skeletonAnimation.state.GetCurrent(0).TimeScale = factor;
    }

    void RecheckColor()
    {
        Color color = Color.white;
        if (critColorTime > 0) color = Color.red;
        else if (freezeTime > 0) color = Color.blue;
        else if (slowTime > 0) color = Color.cyan;

        if(currentColor != color)
        {
            currentColor = color;
            skeletonAnimation.skeleton.Slots.ForEach(s => s.SetColor(color));
        }
    }

    public void TakeEffect(DamageMessage message)
    {
        if (message.EfData.Effect != EffectType.Non) Debug.Log("Take Ef: " + Enum.GetName(typeof(EffectType), message.EfData.Effect));
        switch (message.EfData.Effect)
        {
            case EffectType.Non:
                break;
            case EffectType.DoubleDMG:
                message.ATK *= 2;
                critColorTime = CritColorTime;
                AudioManager.Instance.PlaySFX("crit");
                break;
            case EffectType.SlowDown:
                slowTime = EffectiveTime;
                AudioManager.Instance.PlaySFX("slow");
                break;
            case EffectType.Freeze:
                slowTime = EffectiveTime;
                AudioManager.Instance.PlaySFX("freeze");
                break;
        }
    }

    private void OnDisable()
    {
        freezeTime = 0;
        slowTime = 0;
        if(zombieData != null) zombieData.CurrentMoveSpeed = zombieData.MoveSpeedOrigin;
        skeletonAnimation.skeleton.Slots.ForEach(s => s.SetColor(Color.white));
        skeletonAnimation.state.GetCurrent(0).TimeScale = 1f;
    }
}
