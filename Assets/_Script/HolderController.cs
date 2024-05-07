using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderController : MonoBehaviour
{
    public static HolderController Instance { private set; get; }

    Dictionary<string, Transform> holders;

    private void Awake()
    {
        Instance = this;
        holders = new Dictionary<string, Transform>();
        var children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            holders.Add(child.name, child);
        }
    }

    public Transform GetByName(string holderName) => holders[holderName];


}
