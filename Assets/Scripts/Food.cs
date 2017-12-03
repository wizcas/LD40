/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour 
{
    public GameObject instance;

    public int takenHealth;
    public int takenSanity;

    public float spawnCheckInterval = 10;
    public float spawnChance = .3f;

    float _nextSpawnCheckTime;

    private void Start()
    {
        instance.SetActive(false);
        _nextSpawnCheckTime = Time.time + spawnCheckInterval;
    }

    private void Update()
    {
        if (_nextSpawnCheckTime < 0) return;
        if(Time.time >= _nextSpawnCheckTime)
        {
            _nextSpawnCheckTime = Time.time + spawnCheckInterval;
            var rnd = Random.value;
            if (rnd < spawnChance)
            {
                instance.SetActive(true);
                _nextSpawnCheckTime = -1;
            }
        }
    }

    public void Take()
    {
        instance.SetActive(false);
        _nextSpawnCheckTime = Time.time + spawnCheckInterval;
    }
}
