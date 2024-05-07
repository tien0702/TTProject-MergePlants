using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterBtn : MonoBehaviour
{
    [SerializeField] private Image rada;
    [SerializeField] private Image circleRada;
    void Start()
    {
        circleRada.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1.2f)
            .SetEase(Ease.OutFlash)
            .SetLoops(-1);
    }

}
