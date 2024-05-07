using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tableName, itemDescription, time, btnDescription, priceText;
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private Image fill;
    [SerializeField] private Button moreBtn;
    bool isUsing = false;
    BoostItem itemTarget;
    private void Awake()
    {
        panel.gameObject.SetActive(false);
    }

    public void BounceUp(BoostItem item)
    {
        if (isUsing) { return; }
        this.itemTarget = item;
        isUsing = true;
        gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.4f).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });

        time.text = FormatClock((int)item.Data.Duration);
        btnDescription.text = string.Format("Add more {0}", FormatSecondsToTime(item.Data.MoreTime));


        priceText.text = item.Data.Price.ToString();
        if (item.Data.Price > Player.GetInstance().GetDiamond())
            moreBtn.interactable = false;
        else
            moreBtn.interactable = true;

        fill.fillAmount = (item.Data.Duration / item.Data.MaxDuration);
    }
    public void BuyMoreItem()
    {
        if (itemTarget.Data.Duration + itemTarget.Data.MoreTime > itemTarget.Data.MaxDuration)
        {
            HUDLayer.Instance.Notify("TimeLimit");
        }
        else
        {
            time.text = FormatClock((int)itemTarget.Data.Duration);
            fill.fillAmount = (itemTarget.Data.Duration / itemTarget.Data.MaxDuration);
            itemTarget.Data.Quantity += 1;
            itemTarget.Use();
        }
    }

    public void Hide()
    {
        Time.timeScale = 1f;
        panel.gameObject.SetActive(false);
        transform.DOScale(0f, 0.5f).OnComplete(() =>
        {
            isUsing = false;
            gameObject.SetActive(false);
        });
    }

    string FormatClock(int seconds)
    {
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        int s = seconds % 60;

        string formattedTime = hours > 0 ? hours.ToString("00") + ":" : "";
        formattedTime += minutes.ToString("00") + ":" + s.ToString("00");
        return formattedTime;
    }

    string FormatSecondsToTime(int seconds)
    {
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        int s = seconds % 60;

        if (hours > 0) return string.Format("{0} hours", hours.ToString());
        else if (minutes > 0) return string.Format("{0} minutes", minutes.ToString());
        else return string.Format("{0} seconds", s.ToString());
    }

}
