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
    public Transform fromPos;
    public Transform toPos;
    public float speed = 1f;
    public float height = .5f;

    [Header("Game Values")]
    public int initRage;
    public int maxRage;
    public int deltaDelight;
    public int deltaTemper;
    [EnumArray(typeof(TalkType))] public TalkList[] talks;
    public Projectile[] objectPrefabs;

    [Header("Components")]
    public Bubble bubble;
    public SpriteRenderer emotionRdr;
    public Sprite[] rageEmotions;
    public AudioClip maxRageSfx;
    public System.Action<Guest> onMaxRage;

    int _rage;
    Animator _anim;

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
    public bool IsTalking
    {
        get
        {
            if (bubble == null) return false;
            return bubble.IsTalking;
        }
    }

    void Say(TalkType type, System.Action onComplete)
    {
        PlayAnim(type);
        var content = FindTalk(type);
        if(bubble == null)
        {
            PrettyLog.Error("{0} has no bubble!", name);
            return;
        }
        bubble.Say(type, content, onComplete);
    }

    void PlayAnim(TalkType type)
    {
        if (_anim == null)
        {
            _anim = GetComponent<Animator>();
        }
        if (_anim == null) return;
        string stateName = "";
        switch (type) {
            case TalkType.AskForDrink:
                stateName = "Toss";
                break;
            case TalkType.Positive:
                stateName = "Taken";
                break;
            case TalkType.Negative:
            case TalkType.PissedOff:
                stateName = "Refused";
                break;
        }
        if (string.IsNullOrEmpty(stateName)) return;
        _anim.Play(stateName, 0);
    }

    [See]
    public void Toss()
    {
        Say(TalkType.AskForDrink, 
            () =>
            {
                if (fromPos == null)
                    fromPos = transform;
                if (toPos == null)
                    toPos = Camera.main.transform;
                var from = fromPos.position;
                var to = toPos.position;
                var objectPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
                var proj = Instantiate(objectPrefab, from, Quaternion.identity);
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
        emotionRdr.sprite = rageEmotions[GetRageLevel()];

        if (Rage >= maxRage)
        {
            if (onMaxRage != null)
            {
                onMaxRage(this);
            }
            Say(TalkType.PissedOff, () =>
            {
                PlaySfx(transform.parent.parent, maxRageSfx);
                gameObject.SetActive(false);
            });
        }
        else
        {
            Say(isHappy ? TalkType.Positive : TalkType.Negative, null);
        }
    }

    int GetRageLevel()
    {
        var rageFactor = Rage / (float)maxRage;
        return Mathf.Clamp(Mathf.FloorToInt(rageFactor * 10) / 3, 0, rageEmotions.Length - 1);
    }
    
    string FindTalk(TalkType type)
    {
        var candidates = talks[(int)type];
        if (candidates == null || candidates.list.Length == 0)
            return "<color=red>(Blah Blah Blah)</color>";

        return candidates.list[Random.Range(0, candidates.list.Length)];
    }

    void PlaySfx(Transform target, AudioClip clip)
    {
        var audioSrc = target.GetComponent<AudioSource>();
        if (audioSrc != null && clip != null)
        {
            audioSrc.PlayOneShot(clip);
        }
    }

}
