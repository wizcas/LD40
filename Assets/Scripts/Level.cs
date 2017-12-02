/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Singleton<Level>
{
    public int goalMoney;
    public float time;

    public void OnStatUpdated(PlayerStat stat)
    {
        var end = GameEndType.None;
        if(stat.money >= goalMoney)
        {
            end |= GameEndType.MeetGoal;
        }
        if(stat.health <= 0)
        {
            end |= GameEndType.Die;
        }
        if(stat.sanity <= 0)
        {
            end |= GameEndType.Insane;
        }
        if(end > GameEndType.None)
            GameOver(end);
    }

    public void GameOver(GameEndType end) {
        PrettyLog.Log("Game Over: {0}", end);        
    }
}

[System.Flags]
public enum GameEndType
{
    None = 0,
    MeetGoal = 1 <<0,
    Die = 1 << 1,
    Insane = 1 << 2,
    AllLeft = 1 << 3
}
