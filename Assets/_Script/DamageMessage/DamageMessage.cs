using System;

public class DamageMessage
{
    public int ATK { set; get; }
    public bool IsCrit { set; get; }
    public EffectData EfData { set; get; }
    public Ability ATK_Ability { set; get; }

    public DamageMessage(int atk, Ability ability)
    {
        this.ATK = atk;
        EfData = new EffectData() { Duration = 1f };
        if (UnityEngine.Random.Range(0f, 1f) > ability.SuccessRate) this.EfData.Effect = EffectType.Non;
        else this.EfData.Effect = ability.Effect;
        IsCrit = (EfData.Effect == EffectType.DoubleDMG);
    }
}
