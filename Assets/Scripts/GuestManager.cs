/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour 
{
    SpriteRenderer[] renderers;
    Camera cam;

    private void Awake()
    {
        renderers = this.GetComponentsInAllChildren<SpriteRenderer>();
        cam = Camera.main;
    }

    private void Update()
    {
        foreach(var rdr in renderers)
        {
            rdr.transform.rotation = cam.transform.rotation;
        }
    }
}
