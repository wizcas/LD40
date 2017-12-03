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

public class Guest : MonoBehaviour
{
    public Projectile projPrefab;
    public Transform fromPos;
    public Transform toPos;
    public float speed = 1f;
    public float height = .5f;

    [Header("Game Values")]
    public int initRage;
    public int maxRage;
    public int deltaDelight;
    public int deltaTemper;

    [Header("Components")]
    [SerializeField] Bubble bubble;

    public System.Action<Guest> onMaxRage;

    int _rage;
    [See]
    public int Rage
    {
        get { return _rage; }
        private set
        {
            var isHappy = value <= _rage;
            _rage = value;
            OnRageChanged(isHappy);
        }
    }

    [See]
    public bool IsTalking { get { return bubble.IsTalking; } }

    [See]
    public void Toss()
    {
        bubble.Say(TalkType.AskForDrink,
            () =>
            {
                if (fromPos == null)
                    fromPos = transform;
                if (toPos == null)
                    toPos = Camera.main.transform;
                var from = fromPos.position;
                var to = toPos.position;                
                var proj = Instantiate(projPrefab, from, Quaternion.identity);
                proj.owner = this;
                proj.Fly(from, to, height, speed);
            });
    }

    public void OnTaken()
    {
        Rage -= deltaDelight;
    }

    public void OnRefused()
    {
        Rage += deltaTemper;
    }

    void OnRageChanged(bool isHappy)
    {
        if (Rage >= maxRage)
        {
            if (onMaxRage != null)
            {
                onMaxRage(this);
            }
            bubble.Say(TalkType.PissedOff, () =>
            {
                gameObject.SetActive(false);
            });
        }
        else
        {
            bubble.Say(isHappy ? TalkType.Positive : TalkType.Negative);
        }
    }
}
