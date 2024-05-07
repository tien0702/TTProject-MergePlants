using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlantStore : MonoBehaviour
{
    #region Singleton
    public static PlantStore Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance.gameObject);
        Instance = this;
    }
    #endregion


    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private BoxPlant boxPlantPrefab;
    [SerializeField] private CanvasGroup panel;
    List<BoxPlant> boxPlants = new List<BoxPlant>();

    bool isUsing = false;
    bool isLoadedData = false;

    public void Popup()
    {
        if (isUsing) return;
        if(!isLoadedData) isLoadedData = LoadData();

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
        var flowers = FlowerManager.GetInstance().GetFlowers();
        flowers.ForEach(flower =>
        {
            var box = Instantiate(boxPlantPrefab);
            box.InitForFlower(flower);
            boxPlants.Add(box);
            box.transform.SetParent(scrollViewContent.transform);
        });
        return true;
    }
}
