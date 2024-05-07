using DG.Tweening;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;
    bool isUsing = false;
    bool isLoadedData = false;

    public void Popup()
    {
        if (isUsing) return;
        if (!isLoadedData) isLoadedData = LoadData();
        isUsing = true;
        gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.4f).OnComplete(() =>
        {
            Time.timeScale = 0f;
        });
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

    bool LoadData()
    {
        return true;
    }

}
