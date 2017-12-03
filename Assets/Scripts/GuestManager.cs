/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestManager : MonoBehaviour 
{
    public Bubble[] seatBubbles;
    List<Guest> _guests;
    Camera _cam;

    private void Awake()
    {
        InitGuests();
        InitCam();
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
    void InitCam()
    {
        _cam = Camera.main;
    }
    void FaceCamera()
    {
        foreach (var guest in _guests)
        {
            guest.transform.rotation = _cam.transform.rotation;
        }
    }
    #endregion

    #region Guest Action

    public float actionInterval = 1;
    public float startWait = 1f;

    float _nextActionTime;
    int _maxActorCount = 2;

    void InitGuests()
    {
        _guests = this.GetComponentsInAllChildren<Guest>().ToList();
        int index = 0;
        foreach(var guest in _guests)
        {
            guest.onMaxRage = OnMaxRage;
            guest.bubble = seatBubbles[index];
            index++;
        }
    }

    void BeginAction()
    {
        _nextActionTime = Time.time + startWait;
    }

    void UpdateAction()
    {
        if (Level.Instance.isOver) return;

        if(_guests.Count <= 0)
        {
            Level.Instance.GameOver(GameEndType.AllLeft);
            return;
        }
        if(Time.time > _nextActionTime)
        {
            DoProject();
            var wait = actionInterval * (1 + (Random.value - .5f) * 2);
            //PrettyLog.Log("<color=lime>wait: {0}s</color>", wait);
            _nextActionTime = Time.time + wait;
        }
    }

    void DoProject()
    {
        //PrettyLog.Log("<color=yellow>do project</color>");
        var idleGuests = _guests.Where(g => !g.IsTalking);
        var pickedCount = Random.Range(0, _maxActorCount) + 1;
        var totalCount = idleGuests.Count();

        foreach(var actor in idleGuests)
        {
            var poss = (float)pickedCount / totalCount;
            var rnd = Random.value;
            if(rnd <= poss)
            {
                //PrettyLog.Log("poss: {0}, rnd: {1}, pickedCount: {2}", poss, rnd, pickedCount);
                pickedCount--;
                actor.Toss();
                if (pickedCount <= 0) return;
            }
            totalCount--;
        }
    }

    void OnMaxRage(Guest guest)
    {
        _guests.Remove(guest);
    }
    #endregion
}
