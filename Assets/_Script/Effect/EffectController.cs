using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] private List<Effect> effectPrefabs = new List<Effect>();

    private void Start()
    {
        effectPrefabs.ForEach(effect => {
            var pool = new Pool();
            pool.Tag = effect.TagInPool;
            pool.Size = 10;
            pool.prefab = effect.gameObject;
            ObjectPooler.Instance.InitForPool(pool);
        });
    }
}
