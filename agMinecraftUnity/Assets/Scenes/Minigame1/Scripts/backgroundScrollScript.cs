using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class backgroundScrollScript : MonoBehaviour
{
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float time = 0f;
    private float scoreTime = 0f;
    void Update()
    {
        this.transform.Translate(new Vector3(-10f,0,0)*Time.deltaTime, Space.World);
        time += Time.deltaTime;
        scoreTime += Time.deltaTime;
        if (time > 13.6f) {
            time = 0f;
            this.transform.position = Vector3.zero;
            //For good looping, take farthest x coordinate and divide by time.
        }
        //Controls the score counter. Why it's here with the background...Well, the counter is in the background, I guess
        score.text = "Score: " + Math.Floor(scoreTime).ToString();
    }
}
