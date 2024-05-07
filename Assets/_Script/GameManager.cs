using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { private set; get; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    #endregion

    #region Observer
    public static Action OnLaunchGame = () => { };
    #endregion

    void Start()
    {
        // init singletons
        Player.GetInstance();
        ZombieManager.GetInstance();
        FlowerManager.GetInstance();
        FlowerManager.GetInstance().LoadData();
        ChapterManager.Instance.StartChapter(0);
        OnLaunchGame.Invoke();
    }

    void Update()
    {

    }

    public void QuitGame()
    {
        Player.GetInstance().SaveData();
    }
}
