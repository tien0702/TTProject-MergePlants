using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageController
{
    public StageData Data { get; private set; }
    public bool IsPassed = false;
    int waveIndex = 0;
    WaveController currentWave;

    public int WaveIndex => waveIndex;
    public int NumberWaves => Data.Waves.Count;

    bool waiting = false;

    public StageController(StageData data)
    {
        this.Data = data;
    }

    public void Update()
    {
        if (currentWave.IsPassed && !waiting) OnEndWave();
    }

    public void StartStage()
    {
        waveIndex = 0;
        StartWaveByIndex(waveIndex);
    }

    void NextWave()
    {
        StartWaveByIndex(waveIndex + 1);
    }

    void EndStage()
    {
        IsPassed = true;
    }

    void StartWaveByIndex(int index)
    {
        waveIndex = index;
        WaveController wave = new WaveController(Data.Waves[waveIndex]);
        currentWave = wave;
        currentWave.StartWave();
        waiting = false;
    }

    public void OnEndWave()
    {
        waiting = true;
        HUDLayer.Instance.TimeLine.UpdateWaveInfo();
        if (waveIndex == Data.Waves.Count - 1)
        {
            HUDLayer.Instance.ShowState(true, EndStage);
        }
        else
        {
            HUDLayer.Instance.ShowState(true, NextWave);
        }
    }

}
