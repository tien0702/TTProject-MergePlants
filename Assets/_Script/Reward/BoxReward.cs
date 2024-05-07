using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType
{
    ChestReward,
    LuckyReward
}

public class BoxReward : MonoBehaviour
{
    [SerializeField] private Sprite lockBg, openBg;
    [SerializeField] private Image itemAvatar;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button openBtn, buyBtn;

    public bool IsOpened { get; set; }
    RewardItem reward;

    public bool InitWithItem(RewardItem reward)
    {
        this.reward = reward;
        priceText.text = reward.Price.ToString() + " Open";
        return true;
    }

    public void ShowBtn(string btn)
    {
        openBtn.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        if(btn == "Open") openBtn.gameObject.SetActive(true);
        else if (btn == "Buy") buyBtn.gameObject.SetActive(true);
    }

    public virtual void Open()
    {
        if (IsOpened) return;
        IsOpened = true;
        Debug.Log("Open");
    }
}
