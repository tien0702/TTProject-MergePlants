using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    #region Singleton
    public static ChapterManager Instance { private set; get; }

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

    [SerializeField] private List<ChapterData> chapters = new List<ChapterData>();
    public ChapterController chapter { private set; get; }
    int chapterIndex;

    public int StageIndex => chapter.StateIndex;
    public int NumberStage => chapter.NumberStages;
    public int WaveIndex => chapter.CurrentStage.WaveIndex;
    public int NumberWaves => chapter.CurrentStage.NumberWaves;

    private void Update()
    {
        if (chapter == null) return;
        chapter.Update();
    }

    public void StartChapter(int chapterIndex)
    {
        chapter = new ChapterController(chapters[chapterIndex]);
        chapter.StartChapter();
    }
}
