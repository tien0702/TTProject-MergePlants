using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class HUDLayer : MonoBehaviour
{
    public static HUDLayer Instance { get; private set; }

    public ChapterTimeLine TimeLine { get; private set; }
    public PopupBox Popup { get; private set; }
    public LuckyReward LuckyRewardLayer { get; private set; }

    TextMeshProUGUI notifyObject, goldInfo, diamonInfo;
    Player playerInstance;

    [SerializeField] private WaveStateAni success, defeat;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance.gameObject);
        Instance = this;
    }

    void Start()
    {
        TimeLine = transform.Find("ChapterTimeLine").GetComponent<ChapterTimeLine>();
        Popup = transform.Find("PopupBox").GetComponent<PopupBox>();
        notifyObject = transform.Find("Top").Find("Notify").GetComponent<TextMeshProUGUI>();
        goldInfo = transform.Find("Top").Find("GoldInfo").GetComponentInChildren<TextMeshProUGUI>();
        diamonInfo = transform.Find("Top").Find("DiamonInfo").GetComponentInChildren<TextMeshProUGUI>();
        LuckyRewardLayer = transform.Find("LuckyReward").GetComponent<LuckyReward>();
        playerInstance = Player.GetInstance();

        playerInstance.OnGoldChange += OnGoldChange;
        playerInstance.OnDiamonChange += OnDiamonChange;

        OnGoldChange();
        OnDiamonChange();
    }

    public void ShowState(bool state, Action callback)
    {
        if (state)
        {
            success.Show();
            AudioManager.Instance.PlaySFX("win_wave");
            success.OnEndAni += callback;
        }
        else
        {
            defeat.Show();
            defeat.OnEndAni += callback;
        }
    }

    public void Notify(string content)
    {
        notifyObject.text = content;
        notifyObject.gameObject.SetActive(true);
        float yPos = notifyObject.transform.position.y;
        notifyObject.transform
            .DOMove(new Vector3(0, yPos + 100, 0), 0.5f)
            .OnComplete(() =>
            {
                notifyObject.gameObject.SetActive(false);
                notifyObject.transform.position = new Vector3(0, yPos, 0);
            });
    }

    void OnGoldChange()
    {
        goldInfo.text = playerInstance.GetGold().ToString();
        diamonInfo.text = playerInstance.GetDiamond().ToString();
    }

    void OnDiamonChange()
    {
        goldInfo.text = playerInstance.GetGold().ToString();
        diamonInfo.text = playerInstance.GetDiamond().ToString();
    }
}
