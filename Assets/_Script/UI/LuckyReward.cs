using System.Linq;
using UnityEngine;

public class LuckyReward : MonoBehaviour
{
    [SerializeField] private RewardItem[] rewards = new RewardItem[3];
    [SerializeField] private BoxReward[] boxRewards = new BoxReward[3];
    [SerializeField] private GameButton loseBtn;
    [SerializeField] private CanvasGroup panel;

    public void ShowReward(RewardItem[] rewards)
    {
        Time.timeScale = 0;
        this.rewards = rewards;
        for (int i = 0; i < rewards.Length; ++i)
        {
            boxRewards[i].InitWithItem(rewards[i]);
        }
        transform.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        Time.timeScale = 1f;
        transform.gameObject.SetActive(false);
        loseBtn.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
    }

    public void OpenReward(int index)
    {
        if (index < 0 || index >= rewards.Length) return;
        if (boxRewards[index].IsOpened) return;
        boxRewards[index].Open();
        boxRewards[index].ShowBtn("Non");
        for (int i = 0; i < rewards.Length; ++i)
        {
            if (i == index) continue;
            boxRewards[i].ShowBtn("Buy");
        }
        loseBtn.gameObject.SetActive(true);
    }

    public void BuyReward(int index)
    {
        if (index < 0 || index >= rewards.Length) return;
        if (boxRewards[index].IsOpened) return;
        boxRewards[index].Open();
        boxRewards[index].ShowBtn("Non");
        if(!boxRewards.Any(box => !box.IsOpened)) Hide();
    }
}
