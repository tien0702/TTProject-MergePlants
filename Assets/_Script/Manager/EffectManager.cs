using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { set; get; }
    [SerializeField] private Effect1[] effects;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        System.Array.ForEach(this.effects, effect =>
        {
            var pool = new Pool();
            pool.Tag = effect.EffectName;
            pool.Size = 10;
            pool.prefab = effect.Prefab;
            ObjectPooler.Instance.InitForPool(pool);
        });
    }
}
