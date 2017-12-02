/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public GameObject takenScreen;

    private void Start()
    {
        takenScreen.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var proj = other.gameObject.GetComponent<Projectile>();
        proj.Take();
        OnTake(proj);
    }

    void OnTake(Projectile projectile)
    {
        ShowShit();
    }

    void ShowShit()
    {
        StopCoroutine("ShowShitCo");
        StartCoroutine(ShowShitCo());
    }

    IEnumerator ShowShitCo()
    {
        takenScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        takenScreen.gameObject.SetActive(false);
    }
}
