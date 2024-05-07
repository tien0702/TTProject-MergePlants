using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingBtn : GameButton
{
    public CoinType CoinType;
    public int amount;

    protected override void OnEnable()
    {
        int currentCoin = 0;
        if(CoinType == CoinType.GoldType) currentCoin = Player.GetInstance().GetGold();
        else if(CoinType == CoinType.DiamonType) currentCoin = Player.GetInstance().GetDiamond();

        if (currentCoin < amount) this.interactable = false; 
        else this.interactable = true;
    }
}
