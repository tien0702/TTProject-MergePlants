using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStateAni : MonoBehaviour
{
    public Action OnEndAni = () => { };

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void InvokeEnd()
    {
        gameObject.SetActive(false);
        OnEndAni.Invoke();
        OnEndAni = () => { };
    }
}
