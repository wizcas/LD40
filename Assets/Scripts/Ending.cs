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
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject goodEnding;
    public GameObject dieEnding;
    public GameObject insaneEnding;
    public GameObject zombieEnding;
    public GameObject allLeftEnding;
    public GameObject emptyEnding;

    string _rawGoodEndingText;

    private void Awake()
    {
        //_rawGoodEndingText = goodEnding.GetComponentInChildren<Text>().text;
    }

    private void Clear()
    {
        goodEnding.gameObject.SetActive(false);
        dieEnding.gameObject.SetActive(false);
        insaneEnding.gameObject.SetActive(false);
        zombieEnding.gameObject.SetActive(false);
        allLeftEnding.gameObject.SetActive(false);
        emptyEnding.gameObject.SetActive(false);

        //goodEnding.GetComponentInChildren<Text>().text = _rawGoodEndingText;
    }

    public void Show(GameEndType end)
    {
        Clear();
        gameObject.SetActive(true);
        GameObject endingObj = null;

        if ((end.HasFlag(GameEndType.AllLeft)))
        {
            endingObj = allLeftEnding;
        }
        else if (end.HasFlag(GameEndType.MeetGoal))
        {
            endingObj = goodEnding;
            var txt = endingObj.GetComponentInChildren<Text>();
            if (end.HasFlag(GameEndType.Die))
            {
                txt.text += "\n\nBut your body is too severely damaged\nand become useless.\n\nSo your boss fires you.";
            }
            else if (end.HasFlag(GameEndType.Insane))
            {
                txt.text += "\n\n...And died for drunk driving.";
            }
            else
            {
                txt.text += "\n\nYour boss promotes you.\n\nCongratulations!";
            }
        }
        else if (end.HasFlag(GameEndType.Die) || end.HasFlag(GameEndType.Insane))
        {
            if (end.HasFlag(GameEndType.Die | GameEndType.Insane))
            {
                endingObj = zombieEnding;
            }
            else if (end.HasFlag(GameEndType.Die))
            {
                endingObj = dieEnding;
            }
            else if (end.HasFlag(GameEndType.Insane))
            {
                endingObj = insaneEnding;
            }
        }
        if (endingObj == null)
        {
            endingObj = emptyEnding;
        }
        endingObj.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [FlagEnum] public GameEndType testType;
    [See]
    void Test()
    {
        Show(testType);
    }
}
