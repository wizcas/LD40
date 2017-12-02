/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour 
{
    public Vector3 angularVelocity;

    private void Update()
    {
        var av = angularVelocity * Time.deltaTime;
        transform.Rotate(av);
    }

}
