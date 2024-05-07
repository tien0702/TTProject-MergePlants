using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public string id;
    public int Gold { set; get; }
    public int Diamon { set; get; }

    public List<BoostItemData> Items { set; get; }

    public PlayerData() 
    {
        id = string.Empty;
        Items = new List<BoostItemData>();
    }

    public static PlayerData CreateWithTemplate()
    {
        PlayerData data = new PlayerData();
        data.id = string.Empty;
        data.Items = new List<BoostItemData>();
        data.Items.Add(new BoostItemData()
        {
            Type = BoostItemType.X2CoinItem,
            MaxDuration = 300f,
            Price = 3,
            MoreTime = 30,
            Value = 2
        });
        data.Items.Add(new BoostItemData()
        {
            Type = BoostItemType.X10SpeedItem,
            MaxDuration = 300f,
            Price = 3,
            MoreTime = 30,
            Value = 10
        });
        data.Items.Add(new BoostItemData()
        {
            Type = BoostItemType.AngryItem,
            MaxDuration = 300f,
            Price = 3,
            MoreTime = 30,
            Value = 0.5f
        });
        data.Items.Add(new BoostItemData()
        {
            Type = BoostItemType.SlowDownItem,
            MaxDuration = 300f,
            Price = 3,
            MoreTime = 30,
            Value = 0.5f
        });
        data.Items.Add(new BoostItemData()
        {
            Type = BoostItemType.AutoMergeItem,
            MaxDuration = 300f,
            Price = 3,
            MoreTime = 30,
            Value = 2
        });

        return data;
    }
}
