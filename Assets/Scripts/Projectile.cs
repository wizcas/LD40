/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public ParticleSystem breakFxPrefab;

    private void OnMouseDown()
    {
        Break();
    }
    
    void Break()
    {
        Instantiate(breakFxPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Take()
    {
        Destroy(gameObject);
    }
}
