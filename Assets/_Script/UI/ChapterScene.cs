using DG.Tweening;
using UnityEngine;

public class ChapterScene : MonoBehaviour
{
    bool isUsing = false;
    bool isLoadedData = false;
    public void Popup()
    {
        if (isUsing) return;
        if (!isLoadedData) isLoadedData = LoadData();

        isUsing = true;
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        Time.timeScale = 1f;
        isUsing = false;
        gameObject.SetActive(false);
    }

    bool LoadData()
    {
        return true;
    }
}
