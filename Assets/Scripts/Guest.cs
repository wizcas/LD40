/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using Cheers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public Projectile projPrefab;
    public Transform fromPos;
    public Transform toPos;
    public float speed = 1f;
    public float height = .5f;
    public Bubble bubble;

    [Header("Game Values")]
    public int initRage;
    public int maxRage;
    public int deltaDelight;
    public int deltaTemper;

    public System.Action<Guest> onMaxRage;

    int _rage;
    [See]
    public int Rage
    {
        get { return _rage; }
        private set
        {
            _rage = value;
            OnRageChanged();
        }
    }

    [See]
    public void Toss()
    {
        if (fromPos == null)
            fromPos = transform;
        if (toPos == null)
            toPos = Camera.main.transform;
        var from = fromPos.position;
        var to = toPos.position;
        var delta = to - from;
        var distance = delta.magnitude;
        var proj = Instantiate(projPrefab, from, Quaternion.LookRotation(delta.normalized));
        proj.owner = this;
        DOTween.To(
            t =>
            {
                var step = (t + 1) * .5f;
                var framePos = from + delta * step;
                framePos.y = -(t * t) * height + height;
                proj.transform.position = framePos;
            },
            -1, 1, distance / speed
            );
        bubble.Say(TalkType.AskForDrink);
    }

    public void OnTaken()
    {
        Rage -= deltaDelight;
        bubble.Say(TalkType.Taken);
    }

    public void OnRefused()
    {
        Rage += deltaTemper;
        bubble.Say(TalkType.Refused);
    }

    void OnRageChanged()
    {
        if (Rage >= maxRage)
        {
            if (onMaxRage != null)
            {
                onMaxRage(this);
                bubble.Say(TalkType.Leaving, () =>
                {
                    gameObject.SetActive(false);
                });
            }
        }
    }
}
