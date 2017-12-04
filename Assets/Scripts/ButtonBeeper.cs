/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBeeper : MonoBehaviour, IPointerClickHandler
{
    public AudioClip clickSfx;

    static AudioSource _audioSrc;
    public static AudioSource AudioSrc
    {
        get
        {
            if(_audioSrc == null)
            {
                var go = new GameObject("ButtonSfxSrc");
                _audioSrc = go.AddComponent<AudioSource>();
                DontDestroyOnLoad(go);
            }
            return _audioSrc;
        }
    }
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSfx != null)
        {
            AudioSrc.PlayOneShot(clickSfx);
        }
    }
}
