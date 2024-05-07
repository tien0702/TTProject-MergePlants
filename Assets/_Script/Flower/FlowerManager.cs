using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlowerManager
{
    #region Singleton
    private static FlowerManager instance;

    public static FlowerManager GetInstance()
    {
        if(instance == null)
        {
            instance = new FlowerManager();
        }
        return instance;
    }

    private FlowerManager()
    {

    }

    #endregion
    public Action OnSpeedMultipChange = () => { };

    private List<FlowerData> Flowers;
    public int MaxLevelUnlocked { private set; get; }

    private float speedMuilti = 1f;
    public float ATKSpeedMultiplier {
        get => speedMuilti;
        set {
            speedMuilti = value;
            OnSpeedMultipChange.Invoke();
        }
    }

    public bool LoadData()
    {
        var FlowerPrefab = Resources.Load<GameObject>("Prefabs/Flower");
        var BulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        Flowers = Resources.LoadAll<FlowerData>("Datas/Flowers/").ToList();
        
        // Init flowers
        var flowerPool = new Pool();
        flowerPool.Tag = "Flower";
        flowerPool.Size = 14;
        flowerPool.prefab = FlowerPrefab;
        ObjectPooler.Instance.InitForPool(flowerPool);

        // Init bullet
        var bulletPool = new Pool();
        bulletPool.Tag = "Bullet";
        bulletPool.Size = 20;
        bulletPool.prefab = BulletPrefab;
        ObjectPooler.Instance.InitForPool(bulletPool);

        return true;
    }

    public Flower GetNextLevel(int currentLv)
    {
        var flower = Flowers.Find(f => f.Level == currentLv + 1);
        if (flower == null) return null;

        var flowerObject = ObjectPooler.Instance.GetObject("Flower").GetComponent<Flower>();
        flowerObject.InitWithData(flower);
        flowerObject.gameObject.SetActive(true);
        return flowerObject;
    }

    public Flower GetByLevel(int level)
    {
        var flower = Flowers.Find(f => f.Level == level);
        if (flower == null) return null;

        var flowerObject = ObjectPooler.Instance.GetObject("Flower").GetComponent<Flower>();
        flowerObject.InitWithData(flower);
        flowerObject.gameObject.SetActive(true);
        return flowerObject;
    }

    public List<FlowerData> GetFlowers() => Flowers;
}
