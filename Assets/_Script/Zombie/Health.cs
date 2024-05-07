using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Observers
    private Dictionary<EventID.HealthEventID, Action<int>> observers = new Dictionary<EventID.HealthEventID, Action<int>>();
    public void Register(EventID.HealthEventID eventID, Action<int> callback)
    {
        if (!observers.ContainsKey(eventID))
        {
            observers.Add(eventID, null);
        }
        observers[eventID] += callback;
    }

    public void Unregister(EventID.HealthEventID eventID, Action<int> callback)
    {
        if (!observers.ContainsKey(eventID))
        {
            //Debug.Log(string.Format("Can't Unregister because HealthEventID {0} not exists!", eventID.ToString()));
            return;
        }
        observers[eventID] -= callback;
    }

    public void PostEvent(EventID.HealthEventID eventID, int param)
    {
        if (!observers.ContainsKey(eventID))
        {
            //Debug.Log(string.Format("Can't PostEvent because HealthEventID {0} not exists!", eventID.ToString()));
            return;
        }

        observers[eventID]?.Invoke(param);
    }

    #endregion
    public int MaxHealth { private set; get; }
    public int CurrentHealth { private set; get;}

    public void SetMaxHP(int hp)
    {
        this.MaxHealth = hp;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        PostEvent(EventID.HealthEventID.OnTakeDMG, amount);
        AudioManager.Instance.PlaySFX("hit");
        if (CurrentHealth == 0)
        {
            OnDead();
        }
    }

    public void Reborn()
    {
        int hpNeed = MaxHealth - CurrentHealth;
        CurrentHealth = MaxHealth;
        PostEvent(EventID.HealthEventID.OnReborn, hpNeed);
    }

    public void Recovery(int amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        PostEvent(EventID.HealthEventID.OnRecovery, amount);
    }

    public void OnDead()
    {
        PostEvent(EventID.HealthEventID.OnDie, gameObject.GetInstanceID());
    }
}
