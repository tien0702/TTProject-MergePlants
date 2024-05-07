using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player
{
    #region Singleton

    private static Player instance;

    public static Player GetInstance()
    {
        if (instance == null)
        {
            instance = new Player();
        }

        return instance;
    }

    private Player()
    {
        LoadData();
    }

    #endregion

    #region Observer

    public Action OnGoldChange = () => { };
    public Action OnDiamonChange = () => { };

    #endregion

    PlayerData data;
    private void LoadData()
    {
        string path = "Assets/Resources/player-data.json";
        if(!File.Exists(path))
        {
            data = PlayerData.CreateWithTemplate();
            SaveData();
        }
        else
        {
            var d = Resources.Load<TextAsset>("player-data");
            data = JsonConvert.DeserializeObject<PlayerData>(d.text);
        }
    }

    public bool GetGoldWithAmount(int amount)
    {
        if (data.Gold < amount) return false;
        data.Gold -= amount;
        OnGoldChange.Invoke();
        return true;
    }

    public bool GetDiamonWithAmount(int amount)
    {
        if (data.Diamon < amount) return false;
        data.Diamon -= amount;
        OnDiamonChange.Invoke();
        return true;
    }

    public int GetGold() => data.Gold;
    public int GetDiamond() => data.Diamon;

    public void AddGold(int amount)
    {
        data.Gold += amount;
        OnGoldChange.Invoke();
    }

    public void AddDiamon(int amount)
    {
        data.Diamon += amount;
        OnDiamonChange.Invoke();
    }

    public bool UseItem(BoostItemType type)
    {
        var item = data.Items.Find(i => i.Type == type);
        if (item == null) return false;
        if (item.Quantity <= 0) return false;
        item.Quantity -= 1;
        return true;
    }
    public BoostItemData GetItemData(BoostItemType type) => data.Items.Find(item => item.Type == type);
    public List<BoostItemData> GetBoostItemDatas() => data.Items;
    public void SaveData()
    {
        string content = JsonConvert.SerializeObject(data);
        File.WriteAllText("Assets/Resources/player-data.json", content);
    }
}
