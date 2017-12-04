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

public class RoomLoader : Singleton<RoomLoader>
{
    public GameObject loadingScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        loadingScreen.SetActive(false);
    }

    public void LoadRoom(int index = 2)
    {
        var async = SceneManager.LoadSceneAsync(index);
        StartCoroutine(DoLoadScene(async));
    }

    IEnumerator DoLoadScene(AsyncOperation async)
    {
        loadingScreen.gameObject.SetActive(true);
        while (!async.isDone)
        {
            yield return null;
        }
        loadingScreen.gameObject.SetActive(false);
    }
}
