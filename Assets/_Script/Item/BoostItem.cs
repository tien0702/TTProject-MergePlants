using TMPro;
using UnityEngine;

public class BoostItem : MonoBehaviour
{
    [field: SerializeField] public BoostItemData Data { set; get; }

    TextMeshProUGUI clock, boxNumber;
    bool isApply = false;

    public void Awake()
    {
        boxNumber = transform.Find("BoxNumber").GetComponentInChildren<TextMeshProUGUI>();
        clock = transform.Find("Clock").GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Start()
    {
        boxNumber.text = Data.Quantity.ToString();
        if (Data.Quantity == 0) { SetActiveBoxNumber(false); }
        if (Data.Duration <= 0) { SetActiveClock(false); }
        else { 
            SetActiveClock(true); 
            ApplyItem();
        }
    }

    public void Update()
    {
        if (Data.Duration <= 0) return;
        Data.Duration -= Time.deltaTime;
        clock.text = FormatClock((int)Data.Duration);
        if (Data.Duration <= 0)
        {
            Data.Duration = 0;
            SetActiveClock(false);
            SetActiveBoxNumber(Data.Quantity > 0);
            DiscardItem();
        }
    }

    void SetActiveBoxNumber(bool active)
    {
        boxNumber.transform.parent.gameObject.SetActive(active);
    }

    void SetActiveClock(bool active)
    {
        clock.transform.parent.gameObject.SetActive(active);
    }

    public void AddMore(int quantity)
    {
        this.Data.Quantity += quantity;
        if (Data.Quantity != 0) { SetActiveBoxNumber(true); }
    }

    public void Use()
    {
        if (Data.Quantity == 0)
        {
            HUDLayer.Instance.Popup.BounceUp(this);
        }
        else
        {
            SetActiveClock(true);
            Data.Quantity -= 1;
            if (Data.Quantity == 0) { SetActiveBoxNumber(false); }
            Data.Duration += Data.MoreTime;
            boxNumber.text = Data.Quantity.ToString();
            if (!isApply)
            {
                isApply = true;
                ApplyItem();
            }
        }
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

    protected virtual void ApplyItem()
    {
        isApply = true;
    }

    protected virtual void DiscardItem()
    {
        isApply = false;
    }
}
