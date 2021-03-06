﻿/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    //public GameObject takenScreen;

    private void Start()
    {
        //takenScreen.gameObject.SetActive(false);
        Init();
    }

    #region Interaction
    private void OnTriggerEnter(Collider other)
    {
        var proj = other.gameObject.GetComponent<Projectile>();
        proj.Take();
        OnTake(proj);
    }

    void OnTake(Projectile projectile)
    {
        UpdateStatByProjectile(projectile, true);
    }

    void CheckRefuse(Vector3 screenPos)
    {
        RaycastHit hit;
        if (Physics.SphereCast(Camera.main.ScreenPointToRay(screenPos), .1f, out hit, 5f, LayerMask.GetMask("Projectile")))
        {
            var proj = hit.collider.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Break();
                UpdateStatByProjectile(proj, false);
            }
            else
            {
                var food = hit.collider.GetComponentInParent<Food>();
                if (food != null)
                {
                    food.Take();
                    UpdateStatByFood(food);
                }
            }
        }
    }

    private void Update()
    {
        if (Level.Instance.isOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            CheckRefuse(Input.mousePosition);
        }
    }
    #endregion

    #region Stat

    public PlayerStat initStat;
    public PlayerStat currentStat;
    public StatChangeEvent OnStatChanged;

    void Init()
    {
        currentStat = initStat;
        currentStat.onChanged = stat =>
        {
            OnStatChanged.Invoke(stat);
        };
        currentStat.Init();
    }

    void UpdateStatByProjectile(Projectile proj, bool isTaken)
    {
        if (isTaken)
            currentStat.Change(proj.takenMoney, proj.takenHealth, proj.takenSanity);
        else
            currentStat.Change(proj.refuseMoney, proj.refuseHealth, proj.refuseSantiy);
    }

    void UpdateStatByFood(Food food)
    {
        currentStat.Change(0, food.takenHealth, food.takenSanity);
    }

    #endregion
}

[System.Serializable]
public class StatChangeEvent : UnityEvent<PlayerStat> { }

[System.Serializable]
public struct PlayerStat
{
    public int money;
    public int health;
    public int sanity;

    [System.NonSerialized] public int deltaMoney;
    [System.NonSerialized] public int deltaHealth;
    [System.NonSerialized] public int deltaSanity;

    public System.Action<PlayerStat> onChanged;

    public float HealthRatio
    {
        get { return (float)health / _origHealth; }
    }

    public float SanityRatio
    {
        get { return (float)sanity / _origSanity; }
    }

    int _origHealth, _origSanity;

    public void Init()
    {
        _origHealth = health;
        _origSanity = sanity;
        if (onChanged != null)
            onChanged(this);
    }

    public void Change(int deltaMoney, int deltaHealth, int deltaSanity)
    {
        this.deltaMoney = deltaMoney;
        this.deltaHealth = deltaHealth;
        this.deltaSanity = deltaSanity;
        money += deltaMoney;
        AddAndClamp(ref health, deltaHealth, _origHealth);
        AddAndClamp(ref sanity, deltaSanity, _origSanity);

        if (onChanged != null)
        {
            onChanged(this);
        }
    }

    void AddAndClamp(ref int val, int delta, int max)
    {
        val = Mathf.Min(val + delta, max);
    }
}
