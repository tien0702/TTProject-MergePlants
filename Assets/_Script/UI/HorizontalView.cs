using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalView : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewContent;

    [SerializeField] private List<Image> chapterIcons;
    bool isLoaded = false;
    private void Awake()
    {
        if (isLoaded) return;
        isLoaded = true;
        chapterIcons = new List<Image>();
        foreach(Transform child in scrollViewContent.transform)
        {
            if (child.gameObject.name == "Start" || child.gameObject.name == "End") return;
            chapterIcons.Add(child.GetComponentInChildren<Image>());
        }
    }

    void Update()
    {

    }
}
