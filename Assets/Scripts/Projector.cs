/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using Cheers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour 
{
    public GameObject objPrefab;
    public Transform fromPos;
    public Transform toPos;
    public float speed = 1f;
    public float height = .5f;

    [See]
    public void Toss()
    {
        if (fromPos == null)
            fromPos = transform;
        if (toPos == null)
            toPos = Camera.main.transform;
        var from  = fromPos.position;
        var to = toPos.position;
        var delta = to - from;
        var distance = delta.magnitude;
        var obj = Instantiate(objPrefab, from, Quaternion.LookRotation(delta.normalized));
        DOTween.To(
            t =>
            {
                var step = (t + 1) * .5f;
                var framePos = from + delta * step;
                framePos.y = -(t * t) * height + height;
                obj.transform.position = framePos;
            },
            -1, 1, distance / speed
            );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toss();
        }
    }
}
