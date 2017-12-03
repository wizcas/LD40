/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatBar : UIBehaviour
{
    public Image barHealth;
    public Image barSanity;
    public Image barMoney;
    public Text valMoney;
    public Text goalMoney;

    public StatTip healthTipPrefab;
    public StatTip sanityTipPrefab;
    public StatTip moneyTipPrefab;
    public RectTransform tipSpawnPos;

    protected override void Start()
    {
        goalMoney.text = Level.Instance.goalMoney.ToString();        
    }

    public void OnStatUpdated(PlayerStat stat)
    {
        barHealth.fillAmount = stat.HealthRatio;
        barSanity.fillAmount = stat.SanityRatio;
        valMoney.text = stat.money.ToString();
        barMoney.fillAmount = (float)stat.money / Level.Instance.goalMoney;

        float delay = 0;
        if (stat.deltaHealth != 0)
        {
            SpawnTip(healthTipPrefab, stat.deltaHealth, delay);
            delay += .5f;
        }
        if (stat.deltaSanity != 0)
        {
            SpawnTip(sanityTipPrefab, stat.deltaSanity, delay);
            delay += .5f;
        }
        if (stat.deltaMoney != 0)
        {
            SpawnTip(moneyTipPrefab, stat.deltaMoney, delay);
            delay += .5f;
        }
    }

    void SpawnTip(StatTip prefab, int value, float delay)
    {
        var tip = Instantiate(prefab);
        tip.transform.SetParent(transform, false);
        tip.Spawn(value, tipSpawnPos.anchoredPosition, delay);
    }
}
