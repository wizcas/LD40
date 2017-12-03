/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using Cheers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Text txtContent;
    public Transform worldPos;
    public Image imgBubble;
    public Vector2 offset;

    [EnumArray(typeof(TalkType))] public Color[] talkTypeColors;

    public RectTransform rectTransform
    {
        get { return transform as RectTransform; }
    }

    public bool IsTalking { get { return gameObject.activeSelf; } }

    Coroutine _hidingCoroutine;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdatePos(Vector2 areaSize)
    {
        var bubbleVP = Camera.main.WorldToViewportPoint(worldPos.position + offset.ToVector3());
        var bubbleSize = rectTransform.rect.size;
        var max = (areaSize - bubbleSize) * .5f;
        var min = -max;
        var bubbleAnchorPos = new Vector2(
            Mathf.Clamp(areaSize.x * (bubbleVP.x - .5f), min.x, max.x),
            Mathf.Clamp(areaSize.y * (bubbleVP.y - .5f), min.y, max.y)
        );
        rectTransform.anchoredPosition = bubbleAnchorPos;
    }

    public void Say(TalkType type, string content, System.Action onComplete)
    {
        if (_hidingCoroutine != null)
            StopCoroutine(_hidingCoroutine);
        if (type == TalkType.None)
        {
            gameObject.SetActive(false);
            return;
        }

        txtContent.text = content;
        var color = talkTypeColors[(int)type];
        if (color == Color.clear) color = Color.white;
        imgBubble.color = color;
        
        gameObject.SetActive(true);
        _hidingCoroutine = StartCoroutine(SayCo(onComplete));
    }

    IEnumerator SayCo(System.Action onComplete)
    {
        yield return new WaitForSeconds(BubbleManager.Instance.sayTime);
        gameObject.SetActive(false);
        if (onComplete != null)
            onComplete();
    }
}
