using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChapterController
{
    public ChapterData Data { get; private set; }
    public bool IsPassed = false;

    public StageController CurrentStage { private set; get; }
    int stageIndex = 0;

    public int StateIndex => stageIndex;
    public int NumberStages => Data.Stages.Count;

    bool waiting = false;
    public ChapterController(ChapterData data)
    {
        this.Data = data;
    }

    public void StartChapter()
    {
        StartStageByIndex(0);
    }

    void NextStage()
    {
        StartStageByIndex(stageIndex + 1);
    }

    void StartStageByIndex(int index)
    {
        stageIndex = index;
        StageController stage = new StageController(Data.Stages[stageIndex]);

        this.CurrentStage = stage;
        this.CurrentStage.StartStage();
    }

    public void Update()
    {
        CurrentStage.Update();
        if (CurrentStage.IsPassed && !waiting) HandleStage();
    }

    public void HandleStage()
    {
        waiting = true;
        if(stageIndex == Data.Stages.Count - 1) EndChapter();
        else NextStage();
        HUDLayer.Instance.TimeLine.UpdateStageInfo();
        waiting = false;
    }


    void EndChapter()
    {
        IsPassed = true;
    }
}
