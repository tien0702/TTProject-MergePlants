using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventID
{
    public enum HealthEventID
    {
        OnTakeDMG,
        OnReborn,
        OnRecovery,
        OnDie
    }

    public static class SkelAniID
    {
        public static readonly string ATTACK_EVENT = "attack";
    }
}
