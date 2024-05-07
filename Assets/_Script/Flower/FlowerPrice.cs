using UnityEngine;

public enum CoinType
{
    GoldType,
    DiamonType
}

[System.Serializable]
public class FlowerPrice
{
    [field: SerializeField] public CoinType Type { private set; get; }
    [field: SerializeField] public int Gold { private set; get; }
    [field: SerializeField] public int Diamon { private set; get; }

}
