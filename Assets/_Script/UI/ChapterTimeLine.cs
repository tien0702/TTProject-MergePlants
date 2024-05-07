using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterTimeLine : MonoBehaviour
{
    [SerializeField] private List<Sprite> chapterAvatar = new List<Sprite>();

    TextMeshProUGUI currentStage, nextStage, waveNumber;
    private void Awake()
    {
        currentStage = transform.Find("CurrentStage").GetComponentInChildren<TextMeshProUGUI>();
        nextStage = transform.Find("NextStage").GetComponentInChildren<TextMeshProUGUI>();
        waveNumber = transform.Find("WaveNumber").GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.OnLaunchGame += UpdateInfo;
    }

    public void UpdateInfo()
    {
        UpdateStageInfo();
        UpdateWaveInfo();
    }

    public void UpdateWaveInfo()
    {
        int numberWave = ChapterManager.Instance.NumberWaves;
        int waveIndex = ChapterManager.Instance.WaveIndex;
        waveNumber.text = string.Format("{0} / {1}", waveIndex + 1, numberWave);
    }

    public void UpdateStageInfo()
    {
        int stateIndex = ChapterManager.Instance.StageIndex + 1;
        currentStage.text = stateIndex.ToString(); 
        nextStage.text = (stateIndex + 1).ToString();
    }
}
