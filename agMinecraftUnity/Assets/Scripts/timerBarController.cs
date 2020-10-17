using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class timerBarController : MonoBehaviour
{
    public GameObject imageTextHolder;
    public UnityEngine.UI.Image timerBar;
    public float maxTime;
    float timeLeft;
    public TextMeshProUGUI activityText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime; //actually adjusts the fill amount
        } else
        {
            imageTextHolder.SetActive(false); //would it make more sense to use a coroutine here?
        }
    }
    public void startTimer(float displayTime, string activity) //public method to call from player object
    {
        activityText.text = activity;
        maxTime = displayTime;
        timeLeft = displayTime;
        imageTextHolder.SetActive(true);
    }
}
