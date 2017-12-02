/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour 
{
    private void Awake()
    {
        InitSpritesAndCam();
        InitProjectors();
    }

    private void Start()
    {
        BeginAction();
    }

    private void Update()
    {
        FaceCamera();
        UpdateAction();
    }

    #region Face the Camera
    SpriteRenderer[] _renderers;
    Camera _cam;
    void InitSpritesAndCam()
    {
        _renderers = this.GetComponentsInAllChildren<SpriteRenderer>();
        _cam = Camera.main;
    }
    void FaceCamera()
    {
        foreach (var rdr in _renderers)
        {
            rdr.transform.rotation = _cam.transform.rotation;
        }
    }
    #endregion

    #region Guest Action

    public float actionInterval = 1;
    public float startWait = 1f;
    Projector[] _actors;
    float _nextActionTime;
    int _maxActorCount = 2;

    void InitProjectors()
    {
        _actors = this.GetComponentsInAllChildren<Projector>();
    }

    void BeginAction()
    {
        _nextActionTime = Time.time + startWait;
    }

    void UpdateAction()
    {
        if(Time.time > _nextActionTime)
        {
            DoProject();
            var wait = actionInterval * (1 + (Random.value - .5f) * 2);
            PrettyLog.Log("<color=lime>wait: {0}s</color>", wait);
            _nextActionTime = Time.time + wait;
        }
    }

    void DoProject()
    {
        PrettyLog.Log("<color=yellow>do project</color>");
        var pickedCount = Random.Range(0, _maxActorCount) + 1;
        var totalCount = _actors.Length;

        foreach(var actor in _actors)
        {
            var poss = (float)pickedCount / totalCount;
            var rnd = Random.value;
            if(rnd <= poss)
            {
                PrettyLog.Log("poss: {0}, rnd: {1}, pickedCount: {2}", poss, rnd, pickedCount);
                pickedCount--;
                actor.Toss();
                if (pickedCount <= 0) return;
            }
            totalCount--;
        }
    }
    #endregion
}
