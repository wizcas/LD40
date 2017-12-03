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
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject goodEnding;
    public GameObject dieEnding;
    public GameObject insaneEnding;
    public GameObject zombieEnding;

    private void Clear()
    {
        goodEnding.gameObject.SetActive(false);
        dieEnding.gameObject.SetActive(false);
        insaneEnding.gameObject.SetActive(false);
        zombieEnding.gameObject.SetActive(false);
    }

    public void Show(GameEndType end)
    {
        Clear();
        gameObject.SetActive(true);
        switch (end)
        {
            case GameEndType.MeetGoal:
                goodEnding.gameObject.SetActive(true);
                break;
            case GameEndType.Die:
                dieEnding.gameObject.SetActive(true);
                break;
            case GameEndType.Insane:
                insaneEnding.gameObject.SetActive(true);
                break;
            case GameEndType.Die | GameEndType.Insane:
                zombieEnding.gameObject.SetActive(true);
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
