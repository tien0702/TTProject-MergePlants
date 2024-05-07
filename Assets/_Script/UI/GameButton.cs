using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButton : Button
{
    protected override void Start()
    {
        base.Start();
        onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        AudioManager.Instance.PlaySFX("button");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

}
