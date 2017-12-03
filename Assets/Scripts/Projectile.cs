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
    public ParticleSystem takeFxPrefab;

    public int takenMoney;
    public int takenHealth;
    public int takenSanity;
    public int refuseMoney;
    public int refuseHealth;
    public int refuseSantiy;

    public Guest owner;

    public AudioClip flySfx;
    public AudioClip breakSfx;
    public AudioClip takenSfx;

    public void Fly(Vector3 from, Vector3 to, float height, float speed)
    {
        var delta = to - from;
        var distance = delta.magnitude;
        transform.rotation = Quaternion.LookRotation(delta.normalized);
        DOTween.To(
            t =>
            {
                var step = (t + 1) * .5f;
                var framePos = from + delta * step;
                framePos.y = -(t * t) * height + height;
                transform.position = framePos;
            },
            -1, 1, distance / speed
            );
        PlaySfx(this, flySfx);
    }

    void PlaySfx(Component target, AudioClip clip)
    {
        var audioSrc = target.GetComponent<AudioSource>();
        if (audioSrc != null && clip != null)
            audioSrc.PlayOneShot(clip);
    }
    
    public void Break()
    {
        if (!Level.Instance.isOver)
        {
            var fx = Instantiate(breakFxPrefab, transform.position, Quaternion.identity);
            PlaySfx(fx, breakSfx);
            owner.OnRefused();
        }
        Destroy(gameObject);
    }

    public void Take()
    {
        if (!Level.Instance.isOver)
        {
            var fx = Instantiate(takeFxPrefab);
            PlaySfx(fx, takenSfx);
            owner.OnTaken();
        }
        Destroy(gameObject);
    }
}
