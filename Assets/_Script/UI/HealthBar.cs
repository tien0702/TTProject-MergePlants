using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image progress;

    Health m_health;
    private void Awake()
    {
        progress = transform.Find("Progress").GetComponent<Image>();
    }
    void Start()
    {
        OnChangeHP(0);
        m_health = transform.parent.GetComponent<Health>();
        m_health.Register(EventID.HealthEventID.OnTakeDMG, OnChangeHP);
        m_health.Register(EventID.HealthEventID.OnReborn, OnChangeHP);
        m_health.Register(EventID.HealthEventID.OnRecovery, OnChangeHP);
    }
    private void OnEnable()
    {
        m_health = transform.parent.GetComponent<Health>();
        m_health.Register(EventID.HealthEventID.OnTakeDMG, OnChangeHP);
        m_health.Register(EventID.HealthEventID.OnReborn, OnChangeHP);
        m_health.Register(EventID.HealthEventID.OnRecovery, OnChangeHP);
    }

    private void OnDisable()
    {
        m_health.Unregister(EventID.HealthEventID.OnTakeDMG, OnChangeHP);
        m_health.Unregister(EventID.HealthEventID.OnReborn, OnChangeHP);
        m_health.Unregister(EventID.HealthEventID.OnRecovery, OnChangeHP);
    }

    void OnChangeHP(int val)
    {
        int maxHp = m_health.MaxHealth;
        int curHp = m_health.CurrentHealth;

        float percent = (curHp * 1f) / (maxHp * 1f);
        progress.fillAmount = percent;
    }
}
