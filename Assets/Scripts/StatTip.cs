/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StatTip : MonoBehaviour 
{
    public Text txtLevel;
    public int[] levelMinValues = new int[] { 0, 10, 20 };
    public AudioClip upSfx;
    public AudioClip downSfx;

    public Tween Spawn(int value, Vector2 ancPos, float delay)
    {
        int level = FindLevel(value);
        if(level <= 0)
        {
            Destroy(gameObject);
            return null;
        }

        txtLevel.text = "";
        for(int i = 0; i < level; i++)
        {
            txtLevel.text += value > 0 ? "<color=#32B367>+</color>" : "<color=#C30000>-</color>";
        }

        var cgrp = GetComponent<CanvasGroup>();
        cgrp.alpha = 0f;
        var rt = transform as RectTransform;
        rt.anchoredPosition = ancPos;
        return DOTween.Sequence().SetDelay(delay)
            .AppendCallback(()=> {
                cgrp.alpha = 1;
                var audioSrc = GetComponent<AudioSource>();
                var sfx = value > 0 ? upSfx : downSfx;
                if (audioSrc != null && sfx != null)
                    audioSrc.PlayOneShot(sfx);
                })
            .Append(rt.DOAnchorPosY(rt.anchoredPosition.y + 200, 1f).SetEase(Ease.OutCubic))
            .Insert(.8f, cgrp.DOFade(0, .2f))
            .OnComplete(() => Destroy(gameObject));
    }

    int FindLevel(int value)
    {
        for(int i = 0; i < levelMinValues.Length; i++)
        {
            var mv = levelMinValues[i];
            var nextMv = i == levelMinValues.Length - 1 ? int.MaxValue : levelMinValues[i + 1];
            var absValue = Mathf.Abs(value);
            if(absValue >= mv && absValue < nextMv)
            {
                return i + 1;
            }
        }
        return 0;
    }
}
