/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [Range(0, 15)]
    public float horizontalViewRange = 10;
    [Range(0, 10)]
    public float verticalViewRange = 5;
    public float anglePerPx = .01f;

    private void Update()
    {
        var screenCenterPos = Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f, 0));
        var delta2Center = Input.mousePosition - screenCenterPos;
        var xAngle = Mathf.Clamp(-delta2Center.y * anglePerPx, -verticalViewRange, verticalViewRange);
        var yAngle = Mathf.Clamp(delta2Center.x * anglePerPx, -horizontalViewRange, horizontalViewRange);

        var viewAngle = new Vector3(xAngle, yAngle, 0f);
        var camRot = Quaternion.Euler(viewAngle);
        Camera.main.transform.rotation = camRot;
    }
}
