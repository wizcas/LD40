/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour 
{
    public void Restart()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RoomLoader.Instance.LoadRoom();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Show()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public bool IsShown { get { return gameObject.activeSelf; } }
}
