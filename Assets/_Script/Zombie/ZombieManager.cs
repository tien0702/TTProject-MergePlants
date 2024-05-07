using System.Collections.Generic;
using UnityEngine;

public class ZombieManager
{
    #region Singleton
    private static ZombieManager Instance;
    public float SpeedMultiplier = 1f;

    [SerializeField] private Zombie zombiePrefab;
    public static ZombieManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new ZombieManager();
        }
        return Instance;
    }

    ZombieManager()
    {
        LoadData();
    }

    #endregion

    List<Zombie> zombiesInGarden = new List<Zombie>();
    bool LoadData()
    {
        zombiePrefab = Resources.Load<Zombie>("Prefabs/Zombie");
        var pool = new Pool();
        pool.Tag = PoolTag.ZOMBIE_TAG;
        pool.Size = 10;
        pool.prefab = zombiePrefab.gameObject;
        ObjectPooler.Instance.InitForPool(pool);

        return true;
    }

    public List<Zombie> ZombiesInGarden()
    {
        return zombiesInGarden;
    }

    public void IntoGarden(Zombie zombie)
    {
        zombiesInGarden.Add(zombie);
    }

    public void OutGarden(Zombie zombie)
    {
        zombiesInGarden.Remove(zombie);
    }
}
