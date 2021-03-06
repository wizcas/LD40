﻿/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using Cheers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleManager : Singleton<BubbleManager>
{
    public Bubble[] bubbles;
    public float sayTime;
    
    Vector2 _canvasSize;
    RectTransform rectTransform
    {
        get { return transform as RectTransform; }
    }    

    void Start()
    {
        _canvasSize = rectTransform.rect.size;
        SetBubblesZOrder();
        SetBubblesPos(true);
    }

    void SetBubblesZOrder()
    {
        var desc = bubbles.OrderByDescending(b => b.worldPos.position.z);
        var index = 0;
        foreach(var b in desc)
        {
            b.transform.SetSiblingIndex(index);
            index++;
        }
    }

    void SetBubblesPos(bool force)
    {
        foreach (var bubble in bubbles)
        {
            if (bubble.gameObject.activeSelf || force)
            {
                bubble.UpdatePos(_canvasSize);
            }
        }
    }

    private void Update()
    {
        SetBubblesPos(false);
    }    
}

public enum TalkType
{
    None,
    AskForDrink,
    Positive,
    Negative,
    PissedOff
}

[System.Serializable]
public class TalkList
{
    public string[] list;
}
