using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxPlant : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI level, ability, power, speed, price;
    [SerializeField] private Image avatar, coinIcon;
    [SerializeField] private Sprite gold, diamon;

    public void InitForFlower(FlowerData flowerData)
    {
        level.text = flowerData.Level.ToString();
        ability.text = GetDescriptionAbility(flowerData.Ability);
        speed.text = flowerData.AttackSpeed.ToString();
        power.text = flowerData.ATK.ToString();

        FlowerPrice flowerPrice = flowerData.Price;
        coinIcon.sprite = (flowerPrice.Type == CoinType.GoldType) ? (gold) : (diamon);
        int priceNum = (flowerPrice.Type == CoinType.GoldType) ? flowerPrice.Gold : flowerPrice.Diamon;
        price.text = priceNum.ToString();

        avatar.sprite = flowerData.Portrait;
        avatar.SetNativeSize();
    }

    string GetDescriptionAbility(Ability ability)
    {
        string description = string.Empty;
        switch (ability.Effect)
        {
            case EffectType.SlowDown:
                description = string.Format("Slow down the enemy for 1 seconds with {0}% change", ability.SuccessRate * 100);
                break;
            case EffectType.Freeze:
                description = string.Format("Freeze the enemy for 1 seconds with {0}% change", ability.SuccessRate * 100);
                break;
            case EffectType.DoubleDMG:
                description = string.Format("Double Damage with {0}% change", ability.SuccessRate * 100);
                break;
        }

        return description;
    }
}
