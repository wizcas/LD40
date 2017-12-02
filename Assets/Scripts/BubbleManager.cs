/*****************************************************
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

    [EnumArray(typeof(TalkType))] public TalkList[] talks;

    Vector2 _canvasSize;
    RectTransform rectTransform
    {
        get { return transform as RectTransform; }
    }    

    void Start()
    {
        _canvasSize = rectTransform.rect.size;
        SetBubblesZOrder();
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

    private void Update()
    {
        foreach(var bubble in bubbles)
        {
            if (bubble.gameObject.activeSelf)
            {
                bubble.UpdatePos(_canvasSize);
            }
        }        
    }    

    public string FindTalk(TalkType type)
    {
        var candidates = talks[(int)type];
        if (candidates == null || candidates.list.Length == 0)
            return "<color=red>(Blah Blah Blah)</color>";

        return candidates.list[Random.Range(0, candidates.list.Length)];
    }
}

public enum TalkType
{
    None,
    AskForDrink,
    Taken,
    Refused,
    Leaving
}

[System.Serializable]
public class TalkList
{
    public string[] list;
}
