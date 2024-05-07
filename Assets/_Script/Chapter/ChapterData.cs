using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChapterData
{
    [field: SerializeField] public string ChapterName { set; get; }
    [field: SerializeField] public List<StageData> Stages = new List<StageData>();
}
