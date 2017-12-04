/*****************************************************
/* Created by Wizcas Chen (http://wizcas.me)
/* Please contact me if you have any question
/* E-mail: chen@wizcas.me
/* 2017 © All copyrights reserved by Wizcas Zhuo Chen
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour 
{
    public GameObject[] steps;
    public int currentStep;

    private void Start()
    {
        NextStep();
    }

    public void NextStep()
    {
        if (currentStep - 1 >= 0 && currentStep - 1 < steps.Length)
        {
            steps[currentStep - 1].SetActive(false);
        }

        if (currentStep < steps.Length)
        {
            steps[currentStep++].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(2);
        }        
    }
}
