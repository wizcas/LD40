/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour 
{
    readonly string[] lines =
    {
        "Enter! ... \nYou come. Good.",
        "You're working overtime today.\nThere is a dinner to attend,\nIt's your job to do business with our guests.",
        "Remember to show them respect.\nBy 'RESPECT', I mean drink with them like hell.\nIt's the ONLY way to make them invest us.",
        "You're driving?\nYour liver is damaged? \nI DON'T GIVE A SHITE!\n\nJust get me the money.",
        "Is that understood? Good.\n\nNow leave."
    };
    public Text txtLine;
    public Tutorial tutorial;

    int _currentLine = 0;

    private void Start()
    {
        tutorial.gameObject.SetActive(false);
        NextLine();
    }

    public void NextLine()
    {
        if(_currentLine < lines.Length)
        {
            txtLine.text = lines[_currentLine++];
        }
        else
        {
            Skip();
        }
    }

    public void Skip()
    {
        tutorial.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
