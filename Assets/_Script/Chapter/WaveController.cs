using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveController
{
    public WaveData Data { get; private set; }
    public bool IsPassed = false;

    [SerializeField] float delaySpawn = 2f;
    List<Zombie> zombies = new List<Zombie>();
    int countZombie;

    public WaveController(WaveData data)
    {
        this.Data = data;
    }

    public void StartWave()
    {
        var holder = HolderController.Instance.GetByName("ZombieHolder");
        for (int i = Data.Zombies.Length - 1; i >= 0; --i)
        {
            var obj = ObjectPooler.Instance.GetObject(PoolTag.ZOMBIE_TAG);
            obj.transform.SetParent(holder.transform);
            var zombie = obj.GetComponent<Zombie>();
            var data = GameObject.Instantiate(Data.Zombies[i]);
            zombie.InitWithData(data);
            zombie.meshRenderer.sortingOrder = i;
            zombies.Add(zombie);
        }

        countZombie = zombies.Count;
        GameManager.Instance.StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        foreach(var zombie in zombies)
        {
            yield return new WaitForSeconds(delaySpawn);
            zombie.gameObject.SetActive(true);
            zombie.m_Move.StartMove();
            ZombieManager.GetInstance().IntoGarden(zombie);
            zombie.m_Health.Register(EventID.HealthEventID.OnDie, HandleZombieDie);
        }
        yield return null;
    }

    void HandleZombieDie(int val)
    {
        var zombie = zombies.Find(z => z.gameObject.GetInstanceID() == val);
        zombie.m_Health.Unregister(EventID.HealthEventID.OnDie, HandleZombieDie);
        --countZombie;

        int giveGold = zombie.Data.Gold * (int)Player.GetInstance().GetItemData(BoostItemType.X2CoinItem).Value;
        Player.GetInstance().AddGold(giveGold);
        FloatingMessage.Instance.FloatingGold(zombie.transform, giveGold);

        if(countZombie == 0) EndWave();
    }

    void EndWave()
    {
        if(Data.Items.Count > 0)
        {
            HUDLayer.Instance.LuckyRewardLayer.ShowReward(Data.Items.ToArray());
        }
        IsPassed = true;
        zombies.Clear();
    }
}
