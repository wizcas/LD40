/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSortingOrderHelper : MonoBehaviour 
{
    private void Awake()
    {
        var rdr = GetComponent<SpriteRenderer>();
        rdr.sortingOrder = rdr.sortingOrder + (int)(-transform.position.z * 100);
    }
}
